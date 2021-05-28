using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Bag
	{
		private List<Gem> Gems = new List<Gem>();
		private Trash Trash { get; set; }
		public Bag(Trash trash) {
			Trash = trash;
			Reset();
		}

		public void Reset() {
			Gems = new List<Gem>();
			Gems.AddRange(Enumerable.Repeat(Gem.Black, 20));
			Gems.AddRange(Enumerable.Repeat(Gem.Blue, 20));
			Gems.AddRange(Enumerable.Repeat(Gem.Cyan, 20));
			Gems.AddRange(Enumerable.Repeat(Gem.Yellow, 20));
			Gems.AddRange(Enumerable.Repeat(Gem.Red, 20));
			Gems = Gems.OrderBy(x => new Random().Next()).ToList();
		}

		/// <summary>
		/// Returns 4 gems from the bag.
		/// </summary>
		public List<Gem> Take(int numberToTake) {
			if (Gems.Count < numberToTake) {
				var trash = Trash.Take();
				Gems.AddRange(trash);
			}
			var result = Gems.Take(numberToTake).ToList();
			Gems = Gems.Skip(numberToTake).ToList();
			return result;
		}

		public bool IsEmpty() => !Gems.Any();
	}
}
