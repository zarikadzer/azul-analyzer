using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Factory : IStorage
	{
		private List<Gem> Gems { get; set; } = new List<Gem>();
		private Heap Heap { get; set; }
		private Bag Bag { get; set; }

		public Factory(Bag bag, Heap heap) {
			Heap = heap;
			Bag = bag;
		}

		public bool IsEmpty() => Gems.Count == 0;
		public Dictionary<Gem, int> GetGems() => Gems.ByColor();

		/// <summary>
		/// Returns how much gems been taken.
		/// </summary>
		public int Take(Gem color) {
			var result = Gems.Where(x => x == color).Count();
			if (result > 0) {
				var restGems = Gems.Where(x => x != color).ToList();
				Heap.Add(restGems);
				Gems = new List<Gem>();
			}
			return result;
		}

		/// <summary>
		/// Tries to fill the factory with gems. If the bag is empty returns false.
		/// </summary>
		public bool Fill() {
			var gems = Bag.Take(4);
			Gems = gems;
			return true;
		}

	}
}
