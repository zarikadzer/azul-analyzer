using System.Collections.Generic;

namespace AzulAnalyzer
{
	public interface IStorage
	{
		int Take(Gem gem);
		bool IsEmpty();
		Dictionary<Gem, int> GetGems();
	}
}