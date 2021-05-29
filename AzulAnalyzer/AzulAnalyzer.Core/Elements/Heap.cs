using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Heap : IStorage
	{
		private List<Tile> Tiles;

		public Heap() {
			Reset();
		}

		public void Reset() {
			Tiles = new List<Tile> {
				Tile.FirstPlayer
			};
		}

		public bool IsEmpty() => Tiles.Count(x => x != Tile.FirstPlayer) == 0;
		public void Add(List<Tile> tiles) => Tiles.AddRange(tiles);
		public Dictionary<Tile, int> GetTiles() => Tiles.ByColor();

		/// <summary>
		/// Returns how much tiles been taken.
		/// </summary>
		public int Take(Tile color) => Tiles.RemoveAll(x => x == color);
	}
}
