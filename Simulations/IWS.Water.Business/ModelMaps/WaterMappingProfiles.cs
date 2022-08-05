using AutoMapper;

namespace IWS.Water.Business.ModelMaps
{
	public class WaterMappingProfiles : Profile
	{
		public WaterMappingProfiles()
		{
			CreateMap<Contracts.Water.WaterTick, Access.Models.WaterTick>();
		}
	}
}