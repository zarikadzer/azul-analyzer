using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Board
	{
		public List<Tile> Floor { get; set; } = new List<Tile>();

		public List<Tile[]> Pattern { get; set; } = new List<Tile[]> {
			new Tile[1] { Tile.None },
			new Tile[2] { Tile.None, Tile.None },
			new Tile[3] { Tile.None, Tile.None, Tile.None },
			new Tile[4] { Tile.None, Tile.None, Tile.None, Tile.None },
			new Tile[5] { Tile.None, Tile.None, Tile.None, Tile.None, Tile.None }
		};

		public List<Tile[]> Wall { get; set; } = new List<Tile[]> {
			new Tile[5] { Tile.None, Tile.None, Tile.None, Tile.None, Tile.None },
			new Tile[5] { Tile.None, Tile.None, Tile.None, Tile.None, Tile.None },
			new Tile[5] { Tile.None, Tile.None, Tile.None, Tile.None, Tile.None },
			new Tile[5] { Tile.None, Tile.None, Tile.None, Tile.None, Tile.None },
			new Tile[5] { Tile.None, Tile.None, Tile.None, Tile.None, Tile.None }
		};

		public void AddToPattern(int row, Tile tile, int tilesNumber) {
			var totalAvailable = Pattern[row].Count(x => x == Tile.None);
			var totalFloor = tilesNumber - totalAvailable;
			if (totalFloor > 0) {
				Floor.AddRange(Enumerable.Repeat(tile, totalFloor));
			}
			for (var i = 0; i < Pattern[row].Length; i++) {
				if (Pattern[row][i] != Tile.None) {
					continue;
				}
				Pattern[row][i] = tile;
			}
		}

		public void AddToFloor(Tile gem, int gemsNumber) => Floor.AddRange(Enumerable.Repeat(gem, gemsNumber));

		/// <summary>
		/// Returns available patterns plates to place tiles depending on colour.
		/// </summary>
		public List<(int row, Tile color, int count)> GetAvailablePatterns() {
			var result = new List<(int, Tile, int)>();
			var sourceGems = Pattern.Select(x => x.FirstOrDefault(g => g != Tile.None)).ToList();
			for (var i = 0; i < 5; i++) {
				result.Add((i, sourceGems[i], Pattern[i].Count(x => x == Tile.None)));
			}
			/// TODO: Add the <see cref="Wall"/> check.
			return result;
		}

		/// <summary>
		/// Checks if a specific pattern line is complete.
		/// A pattern line is complete when all slots are filled with the same color tile.
		/// </summary>
		/// <param name="row">The row index (0-4) of the pattern line to check.</param>
		/// <returns>True if the pattern line is complete, false otherwise.</returns>
		public bool IsPatternLineComplete(int row) {
			if (row < 0 || row >= Pattern.Count) {
				return false;
			}

			var line = Pattern[row];

			// Check if all slots are filled (no Tile.None)
			if (line.Any(tile => tile == Tile.None)) {
				return false;
			}

			// Get the first tile color
			var firstTile = line[0];

			// Validate that the tile is a valid color (not None or FirstPlayer)
			if (firstTile == Tile.None || firstTile == Tile.FirstPlayer) {
				return false;
			}

			// Verify all tiles in the line are the same color
			return line.All(tile => tile == firstTile);
		}

		/// <summary>
		/// Returns a list of row indices for all completed pattern lines.
		/// A completed line has all slots filled with tiles of the same color.
		/// </summary>
		/// <returns>List of row indices (0-4) for completed pattern lines.</returns>
		public List<int> GetCompletedPatternLines() {
			var completedLines = new List<int>();

			for (int i = 0; i < Pattern.Count; i++) {
				if (IsPatternLineComplete(i)) {
					completedLines.Add(i);
				}
			}

			return completedLines;
		}
	}
}
