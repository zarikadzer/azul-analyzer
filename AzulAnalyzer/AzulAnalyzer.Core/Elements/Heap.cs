using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Heap : IStorage
	{
		private List<Gem> Gems;

		public Heap() {
			Reset();
		}

		public void Reset() {
			Gems = new List<Gem>();
		}

		public bool IsEmpty() => Gems.Count == 0;
		public void Add(List<Gem> gems) => Gems.AddRange(gems);
		public Dictionary<Gem, int> GetGems() => Gems.ByColor();

		/// <summary>
		/// Returns how much gems been taken.
		/// </summary>
		public int Take(Gem color) => Gems.RemoveAll(x => x == color);
	}
}
