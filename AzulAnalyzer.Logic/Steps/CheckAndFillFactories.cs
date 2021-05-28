using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class CheckAndFillFactories: BaseStep
	{
		public CheckAndFillFactories(GameContext context) : base(context) {
		}

		protected override void Action(Player player) {
			if (Game.Factories.All(x => x.IsEmpty()) && Game.Heap.IsEmpty()) {
				if (Game.Trash.IsEmpty() && Game.Bag.IsEmpty()) {
					Game.IsGameEnded = true;
					return;
				}
				Game.Factories.ForEach(x => x.Fill());
			}
		}
	}
}
