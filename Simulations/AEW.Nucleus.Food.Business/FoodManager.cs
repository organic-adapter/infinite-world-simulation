using AEW.Contracts.Nucleus.Food;
using AEW.Events.Nucleus.Food;

namespace AEW.Nucleus.Food.Business
{
	public interface FoodManager
	{
		public Task ProcessDemand(FoodDemanded? demand);

		public Task ProcessDemands(IEnumerable<FoodDemanded>? demands);
		public Task DeleteAsync(string id);

		public Task<FoodTick> GetAsync(string id);

		public Task<FoodTick> SaveAsync(FoodTick? waterTick);
	}
}