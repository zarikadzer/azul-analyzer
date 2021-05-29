using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class FillFactories: BaseStep
	{
		public FillFactories(GameContext context) : base(context) {
		}

		public override bool CanPlay() {
			if (Game.Trash.IsEmpty() && Game.Bag.IsEmpty()) {
				Game.IsGameEnded = true;
				return false;
			}
			return Game.Factories.All(x => x.IsEmpty())
				&& Game.Heap.IsEmpty()
				&& (!Game.Trash.IsEmpty() || !Game.Bag.IsEmpty());
		}

		protected override void Action() {
			Game.Factories.ForEach(x => x.Fill());
		}
	}
}
