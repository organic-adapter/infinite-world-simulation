using AEW.Common.Access;
using AEW.Contracts.Nucleus.Food;
using AEW.Events.Nucleus.Food;
using AEW.Nucleus.Food.Access;
using AEW.Prototypes;
using AutoMapper;

namespace AEW.Nucleus.Food.Business
{
	public class AnatomyFoodManager : FoodManager
	{
		private readonly FoodAccess foodAccess;
		private readonly IMapper mapper;
		private readonly NotificationBus nucleusSupplyBus;

		public AnatomyFoodManager
				(
					IMapper mapper,
					NucleusSupplyBus nucleusSupplyBus,
					FoodAccess foodAccess
				)
		{
			this.mapper = mapper;
			this.nucleusSupplyBus = nucleusSupplyBus;
			this.foodAccess = foodAccess;
		}

		public async Task DeleteAsync(string id)
		{
			await foodAccess.RemoveAsync(id);
		}

		public async Task<FoodTick> GetAsync(string id)
		{
			return await foodAccess.RetrieveAsync(id);
		}

		public async Task ProcessDemand(FoodDemanded? demand)
		{
			if (demand == null)
				throw new ArgumentNullException();

			FoodDispatched dispatch = demand.AlwaysDispatchHalfOfWhatIsDemanded<FoodDemanded, FoodDispatched>();

			await foodAccess.SaveAsync(demand);
			await foodAccess.SaveAsync(dispatch);

			await nucleusSupplyBus.PublishAsync(dispatch);
		}

		public async Task ProcessDemands(IEnumerable<FoodDemanded>? demands)
		{
			if (demands == null)
				throw new ArgumentNullException();

			foreach (var demand in demands)
				await ProcessDemand(demand);
		}

		public async Task<FoodTick> SaveAsync(FoodTick? FoodTick)
		{
			var saveMe = mapper.Map<Access.Models.FoodTick>(FoodTick);
			//TODO: tick-history needs to be saved here as well.
			return await foodAccess.SaveAsync(saveMe);
		}
	}
}