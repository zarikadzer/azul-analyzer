using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public enum Gem
	{
		None,
		Red,
		Blue,
		Yellow,
		Black,
		Cyan
	}

	public static class GemExtensions
	{
		public static Dictionary<Gem, int> ByColor(this List<Gem> gems) {
			var result = new Dictionary<Gem, int>();
			gems.ForEach(x => {
				if (!result.ContainsKey(x)) {
					result.Add(x, 1);
				} else {
					result[x]++;
				}
			});
			return result;
		}

		public static (Gem gem, int count) GetMaxGroup(this Dictionary<Gem, int> gems) {
			var maxGroup = gems.Aggregate((max, x) => x.Value > max.Value ? x : max);
			return (maxGroup.Key, maxGroup.Value);
		}

		public static (Gem gem, int count) GetMinGroup(this Dictionary<Gem, int> gems) {
			var minGroup = gems.Aggregate((min, x) => x.Value < min.Value ? x : min);
			return (minGroup.Key, minGroup.Value);
		}
	}
}
