using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class TakeGem : BaseStep
	{
		public TakeGem(GameContext context) : base(context) {
		}

		protected override void Action(Player player) {
			///
			///TODO: Implement logic of taking the best storage.
			///
			IStorage storage = Game.Factories.FirstOrDefault(x => !x.IsEmpty());
			if (storage == default) {
				storage = Game.Heap;
			}
			var gems = storage.GetGems();

			///
			///TODO: Implement logic of taking the best gem.
			///
			var gemsToTake = gems.GetMaxGroup();

			storage.Take(gemsToTake.gem);
			player.AddToSource(gemsToTake.gem, gemsToTake.count);
		}
	}
}
