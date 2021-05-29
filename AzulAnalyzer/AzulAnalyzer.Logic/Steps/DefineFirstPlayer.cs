using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class DefineFirstPlayer : BaseStep
	{
		public DefineFirstPlayer(GameContext context) : base(context) {
		}

		protected override void Action() {
			Game.CurrentPlayer = Game.Players.OrderBy(x => new Random().Next()).FirstOrDefault();
		}
	}
}
