using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Board
	{
		public List<Gem> Floor { get; set; } = new List<Gem>();

		public List<Gem[]> Source { get; set; } = new List<Gem[]> {
			new Gem[1] { Gem.None },
			new Gem[2] { Gem.None, Gem.None },
			new Gem[3] { Gem.None, Gem.None, Gem.None },
			new Gem[4] { Gem.None, Gem.None, Gem.None, Gem.None },
			new Gem[5] { Gem.None, Gem.None, Gem.None, Gem.None, Gem.None }
		};

		public List<Gem[]> Target { get; set; } = new List<Gem[]> {
			new Gem[5] { Gem.None, Gem.None, Gem.None, Gem.None, Gem.None },
			new Gem[5] { Gem.None, Gem.None, Gem.None, Gem.None, Gem.None },
			new Gem[5] { Gem.None, Gem.None, Gem.None, Gem.None, Gem.None },
			new Gem[5] { Gem.None, Gem.None, Gem.None, Gem.None, Gem.None },
			new Gem[5] { Gem.None, Gem.None, Gem.None, Gem.None, Gem.None }
		};

		public void AddToRow(int row, Gem gem, int gemsNumber) {
			var totalAvailable = Source[row].Count(x => x == Gem.None);
			var totalFloor = gemsNumber - totalAvailable;
			if (totalFloor > 0) {
				Floor.AddRange(Enumerable.Repeat(gem, totalFloor));
			}
			for (var i = 0; i < Source[row].Length; i++) {
				if (Source[row][i] != Gem.None) {
					continue;
				}
				Source[row][i] = gem;
			}
		}

		public void AddToFloor(Gem gem, int gemsNumber) => Floor.AddRange(Enumerable.Repeat(gem, gemsNumber));

		/// <summary>
		/// Returns available source plates to place gems depending on colour.
		/// </summary>
		public List<(int row, Gem color, int count)> GetAvailableSources() {
			var result = new List<(int, Gem, int)>();
			var sourceGems = Source.Select(x => x.FirstOrDefault(g => g != Gem.None)).ToList();
			for (var i = 0; i < 5; i++) {
				result.Add((i, sourceGems[i], Source[i].Count(x => x == Gem.None)));
			}
			return result;
		}
	}
}
