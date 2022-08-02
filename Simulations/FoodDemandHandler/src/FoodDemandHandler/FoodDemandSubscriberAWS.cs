using Amazon.Lambda.Core;
using AutoMapper;
using Food.Supply.POC.Contracts;
using FoodDemandHandler.Maps;
using MessageQueues;
using Population.Business;
using Population.Business.Food;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FoodDemandHandler;

public class FoodDemandSubscriberAWS : SubscriberAWS<FoodSupplied, Supply>
{
	private readonly IMapper mapper;
	private readonly PopulationManager populationManager;

	public FoodDemandSubscriberAWS()
	{
		var mapperConfig = new MapperConfiguration((options) => 
		{
			options.AddProfile(typeof(FoodSupplyMappingProfiles));
		});
		mapper = mapperConfig.CreateMapper();
		populationManager = new SimplePopulationManager();
	}

	public override Supply? FunctionHandler(FoodSupplied message, ILambdaContext context)
	{
		return base.FunctionHandler(message, context);
	}

	public override Supply? Handle(FoodSupplied message)
	{
		var payload = message.GetPayload<Supply>();

		populationManager.Feed(mapper.Map<FoodSupply>(payload));

		return payload;
	}
}