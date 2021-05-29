using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public enum Tile
	{
		None,
		Red,
		Blue,
		Yellow,
		Black,
		Cyan,
		FirstPlayer
	}

	public static class TileExtensions
	{
		public static Dictionary<Tile, int> ByColor(this List<Tile> tiles) {
			var result = new Dictionary<Tile, int>();
			tiles.ForEach(x => {
				if (!result.ContainsKey(x)) {
					result.Add(x, 1);
				} else {
					result[x]++;
				}
			});
			return result;
		}

		public static (Tile tile, int count) GetMaxGroup(this Dictionary<Tile, int> tiles) {
			var maxGroup = tiles.Aggregate((max, x) => x.Value > max.Value ? x : max);
			return (maxGroup.Key, maxGroup.Value);
		}

		public static (Tile tile, int count) GetMinGroup(this Dictionary<Tile, int> tiles) {
			var minGroup = tiles.Aggregate((min, x) => x.Value < min.Value ? x : min);
			return (minGroup.Key, minGroup.Value);
		}
	}
}
