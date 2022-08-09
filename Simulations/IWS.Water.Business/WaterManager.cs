using IWS.Contracts.Water;
using IWS.Events.Water;

namespace IWS.Water.Business
{
	public interface WaterManager
	{
		public Task<WaterTick> GetAsync(string id);

		public Task ProcessDemand(WaterDemanded? demand);

		public Task ProcessDemands(IEnumerable<WaterDemanded>? demands);

		public Task<WaterTick> SaveAsync(WaterTick? waterTick);
	}
}