using AEW.Events;
using AEW.Nucleus.Food.Access.Models;

namespace AEW.Nucleus.Food.Access
{
	public interface FoodAccess
	{
		public Task<FoodTick> GetLastTickAsync(string domainName);

		public Task RemoveAsync(string id);

		public Task<FoodTick> RetrieveAsync(string id);

		public Task<FoodTick> SaveAsync(FoodTick waterTick);

		public Task SaveAsync(SupplyDemanded demand);

		public Task SaveAsync(SupplyDispatched dispatch);
	}
}