using AutoMapper;

namespace AEW.Nucleus.Water.Business.ModelMaps
{
	public class WaterMappingProfiles : Profile
	{
		public WaterMappingProfiles()
		{
			CreateMap<Contracts.Nucleus.Water.WaterTick, Access.Models.WaterTick>();
		}
	}
}