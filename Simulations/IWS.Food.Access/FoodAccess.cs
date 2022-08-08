using IWS.Food.Access.Models;

namespace IWS.Food.Access
{
	public interface FoodAccess
	{
		public Task RemoveAsync(string id);

		public Task<FoodTick> RetrieveAsync(string id);

		public Task<FoodTick> SaveAsync(FoodTick waterTick);
	}
}