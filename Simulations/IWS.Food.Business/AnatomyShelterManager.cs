using AutoMapper;
using IWS.Contracts.Food;
using IWS.Food.Access;

namespace IWS.Food.Business
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