using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Game
	{
		private BaseStep StepsChain { get; set; }

		public Game(int players) {
			Context = new GameContext(players);
			InitSteps(Context);
		}
		public GameContext Context{ get; set; }

		private void InitSteps(GameContext context) {
			StepsChain = new CheckAndFillFactories(context)
				.SetNext(new TakeGem(context));
			///
			/// TODO: Implement and attach other steps.
			/// 
		}

		public void Play() {
			var player = Context.Players.First();
			StepsChain.Play(player);
			while (!Context.IsGameEnded) {
				StepsChain.Play(player = player.GetNextPlayer());
			}
			
		}
	}
}
