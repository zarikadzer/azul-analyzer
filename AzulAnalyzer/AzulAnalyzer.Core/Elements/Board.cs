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
	}
}
