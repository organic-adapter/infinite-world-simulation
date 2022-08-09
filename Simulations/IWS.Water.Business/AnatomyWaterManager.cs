using AutoMapper;
using IWS.Common.Access;
using IWS.Contracts.Water;
using IWS.Events.Water;
using IWS.Prototypes;
using IWS.Water.Access;

namespace IWS.Water.Business
{
	public class AnatomyWaterManager : WaterManager
	{
		private readonly NotificationBus coreSupplyBus;
		private readonly IMapper mapper;
		private readonly WaterAccess waterAccess;

		public AnatomyWaterManager
				(
					IMapper mapper,
					CoreSupplyBus coreSupplyBus,
					WaterAccess waterAccess
				)
		{
			this.mapper = mapper;
			this.coreSupplyBus = coreSupplyBus;
			this.waterAccess = waterAccess;
		}

		public async Task<WaterTick> GetAsync(string id)
		{
			return await waterAccess.RetrieveAsync(id);
		}

		public async Task ProcessDemand(WaterDemanded? demand)
		{
			if (demand == null)
				throw new ArgumentNullException();

			WaterDispatched dispatch = demand.AlwaysDispatchWhatIsDemanded<WaterDemanded, WaterDispatched>();

			await waterAccess.SaveAsync(demand);
			await waterAccess.SaveAsync(dispatch);

			await coreSupplyBus.PublishAsync(dispatch);
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