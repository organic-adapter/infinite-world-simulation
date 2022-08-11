using AutoMapper;
using AEW.Contracts.Nucleus.Food;
using AEW.Nucleus.Food.Access;

namespace AEW.Nucleus.Food.Business
{
	public class AnatomyFoodManager : FoodManager
	{
		private readonly IMapper mapper;
		private readonly FoodAccess foodAccess;

		public AnatomyFoodManager
				(
					IMapper mapper,
					FoodAccess FoodAccess
				)
		{
			this.mapper = mapper;
			this.foodAccess = FoodAccess;
		}

		public async Task DeleteAsync(string id)
		{
			await foodAccess.RemoveAsync(id);
		}

		public async Task<FoodTick> GetAsync(string id)
		{
			return await foodAccess.RetrieveAsync(id);
		}

		public async Task<FoodTick> SaveAsync(FoodTick? FoodTick)
		{
			var saveMe = mapper.Map<Access.Models.FoodTick>(FoodTick);
			//TODO: tick-history needs to be saved here as well.
			return await foodAccess.SaveAsync(saveMe);
		}
	}
}