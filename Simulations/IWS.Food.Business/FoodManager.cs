using IWS.Contracts.Food;

namespace IWS.Food.Business
{
	public interface FoodManager
	{
		public Task<FoodTick> GetAsync(string id);

		public Task<FoodTick> SaveAsync(FoodTick? waterTick);
	}
}