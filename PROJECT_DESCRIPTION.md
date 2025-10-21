# Azul Board Game Analyzer

## Overview

Azul Analyzer is a C# implementation of the classic **Azul board game** engine, designed to analyze and determine optimal strategies for playing Azul. The project provides a complete game simulation framework with extensible architecture for implementing AI players and strategy analysis.

## Purpose

The primary goal of this project is to:
- Simulate the Azul board game with accurate game mechanics
- Provide infrastructure for strategy optimization and AI development
- Analyze optimal tile-taking strategies during gameplay
- Support player move evaluation and game state analysis

## Technology Stack

| Component | Technology |
|-----------|-----------|
| **Language** | C# |
| **Runtime** | .NET Core 3.1 (executable), .NET Standard 2.0 (libraries) |
| **Architecture** | Multi-layered with clean separation of concerns |
| **Design Pattern** | Chain of Responsibility for game flow orchestration |

## Project Architecture

```
AzulAnalyzer/
├── AzulAnalyzer/                    # Main Console Application
│   ├── Program.cs                   # Entry point (2-player game launcher)
│   └── AzulAnalyzer.csproj
│
├── AzulAnalyzer.Logic/              # Game Logic & Rules (netstandard2.0)
│   ├── Game/
│   │   ├── Game.cs                  # Game orchestrator & step chain setup
│   │   ├── GameContext.cs           # Game state container
│   │   └── Player.cs                # Player entity with move logic
│   │
│   ├── Steps/                       # Chain of Responsibility pattern
│   │   ├── BaseStep.cs              # Abstract step base class
│   │   ├── DefineFirstPlayer.cs     # Select starting player
│   │   ├── FillFactories.cs         # Populate factory displays
│   │   ├── TakeTile.cs              # Player tile selection logic
│   │   └── SwitchPlayer.cs          # Turn advancement
│   │
│   └── AzulAnalyzer.Logic.csproj    # Project file (depends on Core)
│
└── AzulAnalyzer.Core/               # Domain Models (netstandard2.0)
    ├── Elements/
    │   ├── Tile.cs                  # Tile enumeration (5 colors + FirstPlayer)
    │   ├── Board.cs                 # Player board (Pattern/Wall/Floor areas)
    │   ├── Bag.cs                   # Tile distribution (100 tiles)
    │   ├── Factory.cs               # Factory display (4 tiles)
    │   ├── Heap.cs                  # Central discards + FirstPlayer marker
    │   └── Trash.cs                 # Temporary tile storage
    │
    ├── Interfaces/
    │   └── IStorage.cs              # Storage contract interface
    │
    └── AzulAnalyzer.Core.csproj     # Project file (no dependencies)
```

## Core Components

### Game Elements

#### **Tile** (`Core/Elements/Tile.cs`)
- Represents the colored tiles and markers in Azul
- Enum types: `Red`, `Blue`, `Yellow`, `Black`, `Cyan`, `FirstPlayer`, `None`
- Extension methods for tile grouping and statistics (max/min groups)

#### **Board** (`Core/Elements/Board.cs`)
- **Pattern Area** (5x5): Staging area for tile placement before wall commitment
- **Wall** (5x5): Final scoring area (matches official Azul wall)
- **Floor** (Linear): Penalty area for excess tiles
- Methods: `AddToPattern()`, `AddToFloor()`, `GetAvailablePatterns()`

#### **Bag** (`Core/Elements/Bag.cs`)
- Manages tile distribution (100 tiles: 20 each of 5 colors)
- Automatically refills from trash when depleted
- `Take(numberToTake)`: Returns n random tiles
- `IsEmpty()`: Checks if bag has tiles

#### **Factory** (`Core/Elements/Factory.cs`)
- Display area for player selection during drafting
- Holds 4 tiles at a time
- `Fill()`: Draws 4 tiles from bag
- `Take(color)`: Returns all tiles of a color, moves remaining to heap
- Implements `IStorage` interface

#### **Heap** (`Core/Elements/Heap.cs`)
- Central pool of discarded tiles during selection phase
- Contains the `FirstPlayer` marker token
- `Take(color)`: Removes and returns all tiles of specified color
- `IsEmpty()`: Checks if any non-FirstPlayer tiles remain
- Implements `IStorage` interface

#### **Trash** (`Core/Elements/Trash.cs`)
- Temporary storage for tiles discarded during tile selection
- Used to refill the bag when depleted
- `Take()`: Returns all tiles and clears trash
- Cycles used tiles back into play

### Game Logic

#### **Game** (`Logic/Game/Game.cs`)
- **Orchestrator** of the entire game flow
- Initializes `GameContext` and constructs the step chain
- Uses Chain of Responsibility pattern to sequence game phases
- Step sequence:
  1. `DefineFirstPlayer` → `FillFactories` → `TakeTile`
  2. `SwitchPlayer` → loops back to `FillFactories` until game end

#### **GameContext** (`Logic/Game/GameContext.cs`)
- **State Container** for the entire game session
- Maintains:
  - List of players
  - Current active player
  - All game element collections (Factories, Bag, Heap, Trash)
  - Game state flags (`IsGameEnded`)
- Initializes factories count based on player count: `(2 * players) + 1`

#### **Player** (`Logic/Game/Player.cs`)
- Represents a game participant
- Methods:
  - `AddToFloor(tile)`: Places penalty tiles
  - `AddToPattern(tile, count)`: Places tiles in pattern area (with floor overflow logic)
  - `GetNextPlayer()`: Returns next player in turn order
- Delegates actual placement to associated `Board`

### Step System (Chain of Responsibility)

#### **BaseStep** (`Logic/Steps/BaseStep.cs`)
- Abstract base implementing Chain of Responsibility
- `Action()`: Virtual method overridden by concrete steps
- `CanPlay()`: Guards execution conditions
- `Play()`: Executes action, checks guards, proceeds to next valid step
- Automatically ends game if no valid next steps exist

#### **DefineFirstPlayer** (`Logic/Steps/DefineFirstPlayer.cs`)
- **Condition**: Always executable (entry point)
- **Action**: Randomly selects starting player
- **Next**: `FillFactories` or `TakeTile`

#### **FillFactories** (`Logic/Steps/FillFactories.cs`)
- **Condition**: All factories empty AND heap empty (signals new round)
- **Guard**: Ends game if both bag and trash are empty
- **Action**: Fills all factories with 4 tiles each
- **Next**: `TakeTile`

#### **TakeTile** (`Logic/Steps/TakeTile.cs`)
- **Action**: Current player selects from storage
- **Logic Flow**:
  1. Choose first non-empty factory (or heap if all factories empty)
  2. Select tile color (currently uses max group heuristic)
  3. Take all tiles of that color
  4. Move remaining tiles to heap
  5. If from heap, also take FirstPlayer marker and add to floor
  6. Place selected tiles in pattern or floor
- **Next**: `SwitchPlayer`
- **TODO**: Implement optimal storage selection and tile selection strategy

#### **SwitchPlayer** (`Logic/Steps/SwitchPlayer.cs`)
- **Condition**: Always executable
- **Action**: Advances to next player in circular order
- **Next**: `FillFactories` (triggers new round check)

## Game Flow Diagram

```
Start
  ↓
DefineFirstPlayer (random player selected)
  ↓
FillFactories (load factories with 4 tiles each)
  ↓
TakeTile (current player selects color from storage)
  ↓
SwitchPlayer (move to next player)
  ↓
FillFactories (if factories empty, refill; check end condition)
  ↓
[Loop continues until Bag AND Trash are empty]
  ↓
Game End (IsGameEnded = true)
```

## Interfaces

### IStorage
- **Purpose**: Contract for tile containers (Factory and Heap)
- **Methods**:
  - `Take(Tile color)`: Returns count of tiles removed
  - `IsEmpty()`: Checks if playable tiles remain
  - `GetTiles()`: Returns dictionary of tile counts by color

## Game Initialization

### Default Setup (2-Player Game)
- **Players**: 2
- **Factories**: 5 (formula: 2 × players + 1)
- **Tiles per Factory**: 4
- **Bag Contents**: 100 tiles (20 Red, 20 Blue, 20 Yellow, 20 Black, 20 Cyan)
- **Heap**: 1 FirstPlayer token initially

## Current Implementation Status

### Completed Features
- Game infrastructure and turn-based system
- Tile distribution and shuffling
- Player turn management and switching
- Factory and storage management
- Basic tile selection and placement logic
- Floor penalty area
- Bag/Trash recycling mechanism
- FirstPlayer marker handling

### In Development
- **Strategy Optimization** (TakeTile step)
  - TODO: Implement logic to choose best storage (factory vs. heap)
  - TODO: Implement AI for selecting best tile color based on board state
- **Scoring System**
  - TODO: Implement wall pattern completion detection
  - TODO: Calculate row/column/color bonuses
  - TODO: Add floor penalties calculation
- **Game Completion**
  - TODO: Implement pattern-to-wall tile transfer
  - TODO: Add final scoring step
  - TODO: Implement winner determination
- **AI Players**
  - TODO: Develop strategy evaluation algorithms
  - TODO: Implement Monte Carlo Tree Search (MCTS) or similar
  - TODO: Create benchmark against random players

## Entry Point

**Main Application**: `/AzulAnalyzer/Program.cs`
```csharp
static void Main(string[] args) {
    var game = new Game(players: 2);
    game.Play();
}
```

Creates a 2-player game and executes the game loop until completion.

## Design Patterns Used

| Pattern | Implementation | Purpose |
|---------|----------------|---------|
| **Chain of Responsibility** | BaseStep + concrete steps | Flexible game flow orchestration |
| **Factory Pattern** | GameContext initialization | Creation of game entities |
| **Interface Segregation** | IStorage | Generic tile container contract |
| **Dependency Injection** | Constructor parameters | Loose coupling of components |

## Extensibility Points

1. **New Step Types**: Extend `BaseStep` for pattern completion, scoring, or AI decision-making
2. **Player Strategies**: Override `TakeTile` action or create strategy selector
3. **Board Evaluation**: Add analysis methods to Board class
4. **AI Integration**: Implement strategy evaluators using step chain
5. **Multi-player Support**: Modify `Game` constructor and factory count formula

## Future Enhancement Roadmap

1. **Immediate**: Complete game end conditions and scoring
2. **Short-term**: Implement basic AI strategy (greedy, heuristic-based)
3. **Medium-term**: Advanced AI using MCTS or neural networks
4. **Long-term**: Multi-threading for parallel game simulation, tournament mode

---

This comprehensive project structure provides a solid foundation for Azul game simulation and AI strategy development. The clean architecture allows for easy extension and testing of different player strategies.
