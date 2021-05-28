using System;
using System.Collections.Generic;
using System.Text;

namespace AzulAnalyzer
{
	public abstract class BaseStep
	{
		public BaseStep(GameContext context) {
			Game = context;
		}

		protected GameContext Game;
		protected BaseStep Next { get; set; }

		public BaseStep SetNext(BaseStep next) {
			Next = next;
			return this;
		}

		protected abstract void Action(Player player);

		public virtual bool CanPlay(Player player) => true;

		public virtual void Play(Player player) {
			Action(player);
			if (Next != default && !Game.IsGameEnded && Next.CanPlay(player)) {
				Next.Play(player);
			}
		}
	}
}
