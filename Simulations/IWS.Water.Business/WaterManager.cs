using IWS.Contracts.Water;

namespace IWS.Water.Business
{
	public interface WaterManager
	{
		public Task<WaterTick> GetAsync(string id);

		public Task<WaterTick> SaveAsync(WaterTick? waterTick);
	}
}