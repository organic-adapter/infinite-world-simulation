using AEW.Contracts.Nucleus.Food;

namespace AEW.Nucleus.Food.Business
{
	public interface FoodManager
	{
		public Task DeleteAsync(string id);

		public Task<FoodTick> GetAsync(string id);

		public Task<FoodTick> SaveAsync(FoodTick? waterTick);
	}
}