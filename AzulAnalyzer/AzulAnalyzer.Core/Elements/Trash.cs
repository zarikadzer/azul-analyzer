using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Trash
	{
		private List<Tile> Tiles = new List<Tile>();

		public List<Tile> Take() {
			var result = new List<Tile>();
			if (Tiles.Count > 0) {
				result = Tiles.OrderBy(x => new Random().Next()).ToList();
			}
			Tiles = new List<Tile>();
			return result;
		}

		public bool IsEmpty() => Tiles.Count == 0;
		public void Add(List<Tile> tiles) => Tiles.AddRange(tiles);
		public void Reset() => Tiles = new List<Tile>();
	}
}
