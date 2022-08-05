using AutoMapper;

namespace IWS.Population.Business.ModelMaps
{
	public class PopulationMappingProfiles : Profile
	{
		public PopulationMappingProfiles()
		{
			CreateMap<Contracts.Population.PopulationTick, Access.Models.PopulationTick>();
		}
	}
}