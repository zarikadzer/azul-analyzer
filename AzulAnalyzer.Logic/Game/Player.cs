using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class Player
	{
		private Board Board { get; set; }
		private GameContext GameContext { get; set; }


		public Player(Board board, GameContext gameContext) {
			Board = board;
			GameContext = gameContext;
		}

		internal void AddToSource(Gem gem, int gemsNumber) {
			var available = Board.GetAvailableSources().Where(x=>x.color == gem || x.color == Gem.None).ToList();
			if (available.Count == 0) {
				Board.AddToFloor(gem, gemsNumber);
				return;
			}
			var min = available.Aggregate((currMin, x) => Math.Abs(x.count - gemsNumber) < Math.Abs(currMin.count - gemsNumber) ? x : currMin);
			Board.AddToRow(min.row, gem, gemsNumber);
		}

		public Player GetNextPlayer() {
			var index = GameContext.Players.IndexOf(this);
			var nextIndex = index < (GameContext.Players.Count - 1) ? index + 1 : 0;
			return GameContext.Players[nextIndex];
		}
	}
}
