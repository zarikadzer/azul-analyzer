using System.Collections.Generic;

namespace AzulAnalyzer
{
	public interface IStorage
	{
		int Take(Tile gem);
		bool IsEmpty();
		Dictionary<Tile, int> GetTiles();
	}
}