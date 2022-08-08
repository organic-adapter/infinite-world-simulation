using AutoMapper;
using IWS.Contracts.Water;
using IWS.Water.Access;

namespace IWS.Water.Business
{
	public class AnatomyWaterManager : WaterManager
	{
		private readonly IMapper mapper;
		private readonly WaterAccess waterAccess;

		public AnatomyWaterManager
				(
					IMapper mapper,
					WaterAccess WaterAccess
				)
		{
			this.mapper = mapper;
			this.waterAccess = WaterAccess;
		}

		public async Task<WaterTick> GetAsync(string id)
		{
			return await waterAccess.RetrieveAsync(id);
		}

		public async Task<WaterTick> SaveAsync(WaterTick? WaterTick)
		{
			var saveMe = mapper.Map<Access.Models.WaterTick>(WaterTick);
			//TODO: tick-history needs to be saved here as well.
			return await waterAccess.SaveAsync(saveMe);
		}
	}
}