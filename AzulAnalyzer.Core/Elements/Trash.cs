using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Trash
	{
		private List<Gem> Gems = new List<Gem>();

		public List<Gem> Take() {
			var result = new List<Gem>();
			if (Gems.Count > 0) {
				result = Gems.OrderBy(x => new Random().Next()).ToList();
			}
			Gems = new List<Gem>();
			return result;
		}

		public bool IsEmpty() => Gems.Count == 0;
		public void Add(List<Gem> gems) => Gems.AddRange(gems);
		public void Reset() => Gems = new List<Gem>();
	}
}
