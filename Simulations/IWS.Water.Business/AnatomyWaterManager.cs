using AutoMapper;
using IWS.Contracts.Water;
using IWS.Water.Access;

namespace IWS.Water.Business
{
	public class AnatomyWaterManager : WaterManager
	{
		private readonly IMapper mapper;
		private readonly WaterAccess WaterAccess;

		public AnatomyWaterManager
				(
					IMapper mapper,
					WaterAccess WaterAccess
				)
		{
			this.mapper = mapper;
			this.WaterAccess = WaterAccess;
		}

		public async Task<WaterTick> GetAsync(string id)
		{
			return await WaterAccess.RetrieveAsync(id);
		}

		public async Task<WaterTick> SaveAsync(WaterTick? WaterTick)
		{
			var saveMe = mapper.Map<Access.Models.WaterTick>(WaterTick);
			//TODO: tick-history needs to be saved here as well.
			return await WaterAccess.SaveAsync(saveMe);
		}
	}
}