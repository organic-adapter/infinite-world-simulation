using IWS.Population.Access.Models;

namespace IWS.Population.Access
{
	public interface PopulationAccess
	{
		public Task SaveAsync(PopulationTick populationTick);
	}
}