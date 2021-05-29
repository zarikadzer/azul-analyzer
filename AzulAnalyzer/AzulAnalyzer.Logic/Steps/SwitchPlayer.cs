using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class SwitchPlayer : BaseStep
	{
		public SwitchPlayer(GameContext context) : base(context) {
		}

		protected override void Action() {
			Game.CurrentPlayer = Game.CurrentPlayer.GetNextPlayer();
		}
	}
}
