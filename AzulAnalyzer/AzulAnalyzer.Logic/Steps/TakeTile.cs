using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class TakeTile : BaseStep
	{
		public TakeTile(GameContext context) : base(context) {
		}

		protected override void Action() {
			///
			///TODO: Implement logic of taking the best storage.
			///
			IStorage storage = Game.Factories.FirstOrDefault(x => !x.IsEmpty());

			if (storage == default) {
				storage = Game.Heap;
			}
			var gems = storage.GetTiles();

			///
			///TODO: Implement logic of taking the best tile.
			///
			var tilesToTake = gems.GetMaxGroup();

			var tilesTaken = storage.Take(tilesToTake.tile);
			if (tilesTaken > 0 && storage.Equals(Game.Heap) && storage.Take(Tile.FirstPlayer) > 0) {
				Game.CurrentPlayer.AddToFloor(Tile.FirstPlayer);
			}
			Game.CurrentPlayer.AddToPattern(tilesToTake.tile, tilesTaken);
		}
	}
}
