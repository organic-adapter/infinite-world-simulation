using AutoMapper;

namespace IWS.Food.Business.ModelMaps
{
	public class FoodMappingProfiles : Profile
	{
		public FoodMappingProfiles()
		{
			CreateMap<Contracts.Food.FoodTick, Access.Models.FoodTick>();
		}
	}
}