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

		public void AddToFloor(Tile tile, int tilesNumber = 1) {
			Board.AddToFloor(tile, tilesNumber);
		}

		public void AddToPattern(Tile tile, int tilesNumber) {
			var available = Board.GetAvailablePatterns().Where(x=>x.color == tile || x.color == Tile.None).ToList();
			if (available.Count == 0) {
				Board.AddToFloor(tile, tilesNumber);
				return;
			}
			var min = available.Aggregate((currMin, x) => Math.Abs(x.count - tilesNumber) < Math.Abs(currMin.count - tilesNumber) ? x : currMin);
			Board.AddToPattern(min.row, tile, tilesNumber);
		}

		public Player GetNextPlayer() {
			var index = GameContext.Players.IndexOf(this);
			var nextIndex = index < (GameContext.Players.Count - 1) ? index + 1 : 0;
			return GameContext.Players[nextIndex];
		}
	}
}
