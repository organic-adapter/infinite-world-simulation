using AutoMapper;

namespace AEW.Nucleus.Food.Business.ModelMaps
{
	public class FoodMappingProfiles : Profile
	{
		public FoodMappingProfiles()
		{
			CreateMap<Contracts.Nucleus.Food.FoodTick, Access.Models.FoodTick>();
		}
	}
}