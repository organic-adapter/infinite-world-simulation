using AEW.Common.Access;
using AEW.Events;
using AEW.Nucleus.Water.Access.Models;

namespace AEW.Nucleus.Water.Access
{
	public interface WaterAccess : ResourceAccess
	{
		public Task RemoveAsync(string id);
		public Task<WaterTick> RetrieveAsync(string id);
		public Task<WaterTick> SaveAsync(WaterTick waterTick);
		public Task SaveAsync(SupplyDemanded demand);
		public Task SaveAsync(SupplyDispatched dispatch);

	}
}