# TODO List - Azul Analyzer Project

## 1. Scoring System Implementation

### Task 1.1: Implement Pattern Line Completion Detection
- Add method to Board.cs to check if a pattern line is complete
- Create logic to identify which pattern lines have the required number of tiles
- Validate that all tiles in the line are the same color
- Return list of completed pattern lines for scoring

### Task 1.2: Implement Pattern-to-Wall Tile Transfer
- Create new step class `TransferToWall.cs` in Steps folder
- Extend BaseStep abstract class
- For each completed pattern line, move one tile to the corresponding wall position
- Move remaining tiles from pattern line to trash
- Clear the pattern line after transfer
- Validate wall position is valid for the tile color (following Azul wall pattern)
- Add this step to the game chain between rounds

### Task 1.3: Implement Wall Scoring Logic
- Add scoring calculation method to Board.cs
- Calculate points for newly placed tile on wall
- Count adjacent horizontal tiles (left and right)
- Count adjacent vertical tiles (up and down)
- Apply scoring rules: 1 point for isolated tile, sum of connected tiles in each direction
- Return total score for the placed tile

### Task 1.4: Implement Floor Penalty Calculation
- Add method to calculate floor penalties in Board.cs
- Implement penalty array: -1, -1, -2, -2, -2, -3, -3
- Sum penalties based on number of tiles in floor line
- Clear floor line after penalty calculation
- Return penalty score (negative value)

### Task 1.5: Implement Round Scoring
- Create new step class `ScoreRound.cs` in Steps folder
- Execute after all players complete pattern-to-wall transfers
- For each player, calculate wall points
- For each player, apply floor penalties
- Update player scores
- Add this step to game chain

### Task 1.6: Implement End Game Bonuses
- Add method to calculate bonus points in Board.cs
- Check for completed horizontal rows (+2 points each)
- Check for completed vertical columns (+7 points each)
- Check for completed color sets (+10 points each, all 5 of same color)
- Return total bonus points

### Task 1.7: Implement Final Scoring and Winner Determination
- Create new step class `FinalScoring.cs`
- Check if any player has completed a horizontal row (game end condition)
- Calculate end game bonuses for all players
- Add bonuses to player scores
- Determine winner (highest score)
- Handle tie-breaker rules if needed
- Display final scores and winner

---

## 2. Optimal Strategy Implementation

### Task 2.1: Implement Storage Selection Strategy
- Modify TakeTile.cs step to evaluate all available storages
- Create method to score each storage option (factory vs heap)
- Consider board state when evaluating options
- Consider opponent board states
- Select storage with best strategic value
- Replace current "first non-empty" logic

### Task 2.2: Implement Tile Color Selection Strategy
- Create method to evaluate all available tile colors from selected storage
- Calculate value of each color based on current board state
- Consider pattern line availability
- Consider wall placement opportunities
- Avoid taking tiles that go directly to floor
- Replace current "max group" heuristic

### Task 2.3: Implement Pattern Line Selection Logic
- Create method to determine best pattern line for selected tiles
- Evaluate pattern lines by completion potential
- Consider wall scoring opportunities
- Prefer lines that minimize floor penalties
- Handle overflow tiles strategically

### Task 2.4: Create Board State Evaluator
- Create new class `BoardEvaluator.cs` in Logic folder
- Implement method to score current board position
- Factor in pattern completion progress
- Factor in wall adjacency bonuses
- Factor in floor penalties
- Return numeric evaluation score

---

## 3. AI Player Development

### Task 3.1: Create Player Strategy Interface
- Create IPlayerStrategy.cs interface in Logic/Interfaces folder
- Define method signatures for storage selection
- Define method signatures for tile color selection
- Define method signatures for pattern line selection
- Allow different AI implementations

### Task 3.2: Implement Random Strategy Player
- Create `RandomStrategy.cs` implementing IPlayerStrategy
- Select random storage from available options
- Select random tile color from chosen storage
- Select random valid pattern line
- Use for baseline comparison

### Task 3.3: Implement Greedy Strategy Player
- Create `GreedyStrategy.cs` implementing IPlayerStrategy
- Select storage/color combination that maximizes immediate points
- Prefer moves that complete pattern lines
- Minimize floor penalties
- Short-term optimization only

### Task 3.4: Implement Heuristic-Based Strategy Player
- Create `HeuristicStrategy.cs` implementing IPlayerStrategy
- Weight multiple factors: completion, bonuses, penalties
- Look ahead 1-2 moves
- Balance immediate vs future value
- Consider opponent positions

### Task 3.5: Implement Monte Carlo Tree Search (MCTS) Player
- Create `MCTSStrategy.cs` implementing IPlayerStrategy
- Implement game state tree structure
- Implement selection, expansion, simulation, backpropagation phases
- Configure simulation count and time limits
- Use for optimal strategy determination

### Task 3.6: Create Strategy Comparison Framework
- Create `StrategyTester.cs` utility class
- Run multiple games between different strategies
- Track win rates, average scores, game lengths
- Generate performance statistics
- Output comparison reports

---

## 4. Game Completion Features

### Task 4.1: Implement Game End Condition Check
- Add method to Game.cs to check for horizontal row completion
- Check after each round scoring
- Set IsGameEnded flag when condition met
- Trigger final scoring step

### Task 4.2: Add Player Score Tracking
- Add Score property to Player.cs
- Add method to update score (AddScore)
- Add method to get current score
- Initialize score to 0 at game start

### Task 4.3: Implement Game State Display
- Create method to display current game state to console
- Show each player's board (pattern lines, wall, floor)
- Show current scores
- Show factories and heap state
- Show current player indicator
- Call after each player turn for visibility

### Task 4.4: Add Game History Tracking
- Create GameHistory.cs class to track moves
- Record each player move (storage, color, pattern line)
- Record scores after each round
- Allow replay or analysis of completed games
- Export game history to file

---

## 5. Testing and Validation

### Task 5.1: Create Unit Tests for Core Elements
- Create test project AzulAnalyzer.Tests
- Write tests for Tile operations
- Write tests for Board pattern/wall/floor operations
- Write tests for Bag tile distribution and refilling
- Write tests for Factory and Heap operations
- Write tests for Trash cycling
- Verify all edge cases

### Task 5.2: Create Unit Tests for Game Logic
- Write tests for Game initialization
- Write tests for GameContext setup
- Write tests for Player operations
- Verify factory count formula for different player counts
- Test player switching logic

### Task 5.3: Create Unit Tests for Steps
- Write tests for DefineFirstPlayer step
- Write tests for FillFactories step
- Write tests for TakeTile step
- Write tests for SwitchPlayer step
- Write tests for new scoring steps
- Verify step chain execution order

### Task 5.4: Create Integration Tests
- Test complete 2-player game flow
- Test complete 3-player game flow
- Test complete 4-player game flow
- Verify scoring accuracy
- Verify game end conditions
- Test edge cases (empty bag, multiple simultaneous row completions)

### Task 5.5: Create Strategy Validation Tests
- Test each AI strategy plays valid moves
- Test strategies don't violate game rules
- Compare strategy performance
- Verify MCTS convergence
- Benchmark execution time for each strategy

---

## 6. Code Quality and Documentation

### Task 6.1: Add XML Documentation Comments
- Add XML comments to all public classes in Core
- Add XML comments to all public classes in Logic
- Add XML comments to all public methods
- Document parameters and return values
- Document exceptions

### Task 6.2: Implement Error Handling
- Add try-catch blocks for critical operations
- Validate input parameters in public methods
- Throw meaningful exceptions with descriptive messages
- Handle bag depletion edge cases
- Handle invalid move attempts

### Task 6.3: Code Refactoring
- Review and refactor long methods
- Extract repeated code into helper methods
- Improve variable and method naming
- Ensure consistent code style
- Remove dead code and unused variables

### Task 6.4: Add Logging
- Add logging framework (e.g., Serilog)
- Log game initialization
- Log each player move
- Log scoring calculations
- Log game end and final results
- Configure log levels (Debug, Info, Warning, Error)

### Task 6.5: Update README.md
- Add quick start guide
- Add build instructions
- Add usage examples
- Document command-line options if added
- Add screenshots or example output
- Link to PROJECT_DESCRIPTION.md

---

## 7. Performance Optimization

### Task 7.1: Optimize Tile Selection Performance
- Profile current tile selection logic
- Cache board evaluations when possible
- Optimize storage iteration
- Reduce redundant calculations
- Benchmark improvements

### Task 7.2: Implement Parallel Game Simulation
- Add support for running multiple games concurrently
- Use Task Parallel Library for multi-threading
- Useful for strategy comparison (running 1000s of games)
- Implement thread-safe game state management
- Benchmark performance gains

### Task 7.3: Optimize MCTS Implementation
- Profile MCTS bottlenecks
- Implement transposition tables for repeated states
- Optimize simulation rollouts
- Parallelize tree search if beneficial
- Cache board evaluations

---

## 8. Additional Features (Optional)

### Task 8.1: Add Command-Line Interface
- Add command-line argument parsing
- Support specifying number of players
- Support selecting AI strategies per player
- Support setting simulation count for MCTS
- Support enabling/disabling game state display
- Support exporting game results to file

### Task 8.2: Implement Game Save/Load
- Create serialization for GameContext
- Save game state to JSON file
- Load game state from JSON file
- Resume interrupted games
- Validate loaded game state

### Task 8.3: Add Statistical Analysis Tools
- Create utility to analyze completed games
- Calculate average game length
- Analyze most common winning strategies
- Track tile selection patterns
- Generate visualizations (charts, graphs)

### Task 8.4: Create Web UI
- Create ASP.NET Core web application
- Implement REST API for game operations
- Create browser-based game board visualization
- Allow human vs AI gameplay
- Display real-time game state
- Show AI decision explanations

### Task 8.5: Implement Tournament Mode
- Create tournament bracket system
- Run round-robin or elimination tournaments
- Track cumulative statistics across games
- Generate tournament reports
- Determine overall best strategy

### Task 8.6: Add Support for Azul Variants
- Research Azul: Stained Glass of Sintra variant rules
- Research Azul: Summer Pavilion variant rules
- Create variant-specific classes
- Allow selecting variant at game start
- Adapt AI strategies for variants

---

## Priority Recommendations

**High Priority (Core Functionality):**
- Section 1: Scoring System Implementation (Tasks 1.1-1.7)
- Section 2: Optimal Strategy Implementation (Tasks 2.1-2.4)
- Section 4: Game Completion Features (Tasks 4.1-4.3)

**Medium Priority (AI and Testing):**
- Section 3: AI Player Development (Tasks 3.1-3.5)
- Section 5: Testing and Validation (Tasks 5.1-5.4)
- Section 6: Code Quality and Documentation (Tasks 6.1-6.2)

**Low Priority (Enhancements):**
- Section 3: AI Player Development (Task 3.6)
- Section 5: Testing and Validation (Task 5.5)
- Section 6: Code Quality and Documentation (Tasks 6.3-6.5)
- Section 7: Performance Optimization (All tasks)
- Section 8: Additional Features (All tasks)

---

**Last Updated:** 2025-10-21
