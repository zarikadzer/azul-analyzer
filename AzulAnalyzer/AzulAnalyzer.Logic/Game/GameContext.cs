using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulAnalyzer
{
	public class GameContext
	{
		public GameContext(int playersNumber) => InitGame(playersNumber);

		public List<Factory> Factories = new List<Factory>();
		public List<Player> Players = new List<Player>();
		public Heap Heap { get; set; }
		public Trash Trash { get; set; }
		public Bag Bag { get; set; }

		public bool IsGameEnded { get; set; } = false;

		private void InitGame(int playersNumber) {
			Trash = new Trash();
			Bag = new Bag(Trash);
			Heap = new Heap();

			var factoriesNumber = playersNumber * 2 + 1;
			for (var i = 0; i < factoriesNumber; i++) {
				Factories.Add(new Factory(Bag, Heap));
			}

			for (var i = 0; i < playersNumber; i++) {
				var board = new Board();
				var player = new Player(board, this);
				Players.Add(player);
			}
		}

	}
}
