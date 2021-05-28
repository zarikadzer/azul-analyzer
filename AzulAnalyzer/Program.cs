using System;

namespace AzulAnalyzer
{
	class Program
	{

		static void Main(string[] args) {
			var game = new Game(players: 2);
			game.Play();
		}
	}
}
