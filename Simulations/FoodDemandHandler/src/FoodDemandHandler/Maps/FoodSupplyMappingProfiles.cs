using AutoMapper;
using MessageQueues.Messages;
using Population.Business.Food;
using System.Text.Json;

namespace FoodDemandHandler.Maps
{
	public class FoodSupplyMappingProfiles : Profile
	{
		public FoodSupplyMappingProfiles()
		{
			var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			CreateMap<AwsSqsRecord, MessageBody<FoodSupply>>()
				.ForMember(dest => dest.FQN, opt => opt.Ignore())
				.ForMember(dest => dest.Payload,
								opt => opt.MapFrom(
									src => JsonSerializer.Deserialize<FoodSupply>(src.Body, jsonSerializerOptions)
									)
								);
		}
	}
}