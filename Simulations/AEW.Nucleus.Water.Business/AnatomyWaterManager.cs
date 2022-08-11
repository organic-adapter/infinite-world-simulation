using AEW.Common.Access;
using AEW.Contracts.Nucleus.Water;
using AEW.Events.Nucleus.Water;
using AEW.Nucleus.Water.Access;
using AEW.Prototypes;
using AutoMapper;

namespace AEW.Nucleus.Water.Business
{
	public class AnatomyWaterManager : WaterManager
	{
		private readonly NotificationBus nucleusSupplyBus;
		private readonly IMapper mapper;
		private readonly WaterAccess waterAccess;

		public AnatomyWaterManager
				(
					IMapper mapper,
					NucleusSupplyBus nucleusSupplyBus,
					WaterAccess waterAccess
				)
		{
			this.mapper = mapper;
			this.nucleusSupplyBus = nucleusSupplyBus;
			this.waterAccess = waterAccess;
		}

		public async Task DeleteAsync(string id)
		{
			await waterAccess.RemoveAsync(id);
		}

		public async Task<WaterTick> GetAsync(string id)
		{
			return await waterAccess.RetrieveAsync(id);
		}

		public async Task ProcessDemand(WaterDemanded? demand)
		{
			if (demand == null)
				throw new ArgumentNullException();

			WaterDispatched dispatch = demand.AlwaysDispatchHalfOfWhatIsDemanded<WaterDemanded, WaterDispatched>();

			await waterAccess.SaveAsync(demand);
			await waterAccess.SaveAsync(dispatch);

			await nucleusSupplyBus.PublishAsync(dispatch);
		}

		public async Task ProcessDemands(IEnumerable<WaterDemanded>? demands)
		{
			if (demands == null)
				throw new ArgumentNullException();

			foreach (var demand in demands)
				await ProcessDemand(demand);
		}

		public async Task<WaterTick> SaveAsync(WaterTick? WaterTick)
		{
			var saveMe = mapper.Map<Access.Models.WaterTick>(WaterTick);
			//TODO: tick-history needs to be saved here as well.
			return await waterAccess.SaveAsync(saveMe);
		}
	}
}