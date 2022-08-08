using IWS.Population.Access.Models;

namespace IWS.Population.Access
{
	public interface PopulationAccess
	{
		public Task<PopulationTick> RetrieveAsync(string id);

		public Task<PopulationTick> SaveAsync(PopulationTick populationTick);
	}
}