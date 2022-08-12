using AEW.Contracts.Nucleus.Water;
using AEW.Events.Nucleus.Water;

namespace AEW.Nucleus.Water.Business
{
	public interface WaterManager
	{
		public Task<WaterTick> GetAsync(string id);

		public Task ProcessDemand(WaterDemanded? demand);

		public Task ProcessDemands(IEnumerable<WaterDemanded>? demands);

		public Task<WaterTick> SaveAsync(WaterTick? waterTick);

		public Task DeleteAsync(string id);
	}
}