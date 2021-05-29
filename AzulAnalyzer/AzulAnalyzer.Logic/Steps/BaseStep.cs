using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public abstract class BaseStep
	{
		public BaseStep(GameContext context) {
			Game = context;
		}

		protected GameContext Game;
		protected List<BaseStep> NextSteps { get; set; } = new List<BaseStep>();

		public BaseStep AddNext(params BaseStep[] nextSteps) {
			NextSteps.AddRange(nextSteps);
			return this;
		}

		protected abstract void Action();

		public virtual bool CanPlay() => true;

		public virtual void Play() {
			Action();
			if (NextSteps != default && !Game.IsGameEnded) {
				var next = default(BaseStep);
				foreach (var step in NextSteps) {
					if (step.CanPlay()) {
						next = step;
						break;
					}
				}
				if (next == default) {
					// THE END
					Game.IsGameEnded = true;
					return;
				}
				next.Play();
			}
		}
	}
}
