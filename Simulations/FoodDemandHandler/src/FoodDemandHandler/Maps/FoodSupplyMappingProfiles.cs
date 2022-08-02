using AutoMapper;
using Food.Supply.POC.Contracts;
using Population.Business.Food;

namespace FoodDemandHandler.Maps
{
	public class FoodSupplyMappingProfiles : Profile
	{
		public FoodSupplyMappingProfiles()
		{
			CreateMap<Supply, FoodSupply>();
		}
	}
}