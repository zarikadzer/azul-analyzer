using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Bag
	{
		private List<Tile> Tiles = new List<Tile>();
		private Trash Trash { get; set; }
		public Bag(Trash trash) {
			Trash = trash;
			Reset();
		}

		public void Reset() {
			Tiles = new List<Tile>();
			Tiles.AddRange(Enumerable.Repeat(Tile.Black, 20));
			Tiles.AddRange(Enumerable.Repeat(Tile.Blue, 20));
			Tiles.AddRange(Enumerable.Repeat(Tile.Cyan, 20));
			Tiles.AddRange(Enumerable.Repeat(Tile.Yellow, 20));
			Tiles.AddRange(Enumerable.Repeat(Tile.Red, 20));
			Tiles = Tiles.OrderBy(x => new Random().Next()).ToList();
		}

		/// <summary>
		/// Returns 4 tiles from the bag.
		/// </summary>
		public List<Tile> Take(int numberToTake) {
			if (Tiles.Count < numberToTake) {
				var trash = Trash.Take();
				Tiles.AddRange(trash);
			}
			var result = Tiles.Take(numberToTake).ToList();
			Tiles = Tiles.Skip(numberToTake).ToList();
			return result;
		}

		public bool IsEmpty() => !Tiles.Any();
	}
}
