using IWS.Water.Access.Models;

namespace IWS.Water.Access
{
	public interface WaterAccess
	{
		public Task RemoveAsync(string id);

		public Task<WaterTick> RetrieveAsync(string id);

		public Task<WaterTick> SaveAsync(WaterTick waterTick);
	}
}