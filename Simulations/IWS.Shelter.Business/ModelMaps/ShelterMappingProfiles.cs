using AutoMapper;

namespace IWS.Shelter.Business.ModelMaps
{
	public class ShelterMappingProfiles : Profile
	{
		public ShelterMappingProfiles()
		{
			CreateMap<Contracts.Shelter.ShelterTick, Access.Models.ShelterTick>();
		}
	}
}