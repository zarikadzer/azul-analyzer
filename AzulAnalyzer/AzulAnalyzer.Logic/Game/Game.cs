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
			var fillFactories = new FillFactories(context);
			var takeGem = new TakeGem(context);
			var switchPlayer = new SwitchPlayer(context);

			fillFactories.AddNext(takeGem);
			takeGem.AddNext(switchPlayer);
			switchPlayer.AddNext(fillFactories, takeGem);

			StepsChain =
				new DefineFirstPlayer(context)
					.AddNext(fillFactories, takeGem);
			///
			/// TODO: Implement and attach other steps.
			///
		}

		public void Play() {
			StepsChain.Play();
		}
	}
}
