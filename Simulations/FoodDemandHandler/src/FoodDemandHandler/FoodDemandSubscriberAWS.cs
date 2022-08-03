using Amazon.Lambda.Core;
using AutoMapper;
using FoodDemandHandler.Maps;
using MessageQueues;
using MessageQueues.Messages;
using Population.Business;
using Population.Business.Food;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FoodDemandHandler;

public class FoodDemandSubscriberAWS : SubscriberAWS
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

	//public Stream FunctionHandler(Stream stream, ILambdaContext context)
	//{
	//	var resource = new FoodS3Resource();

	//	using(var reader = new StreamReader(stream))
	//	{
	//		var message = reader.ReadToEnd();
	//		resource.SaveAsync(message).Wait();
	//		stream.Position = 0;
	//	}

	//	return stream;

	//}
	public override AwsHandlerResponse FunctionHandler(AwsSnsMessage message, ILambdaContext context)
	{
		var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
		var records = message.Records;
		IEnumerable<FoodSupply> supplies =
				records
					.Select(record =>
							{
								var obj = JsonSerializer.Deserialize<MessageBody<FoodSupply>>(record.Sns.Message, jsonSerializerOptions);
								if (obj == null || obj.Payload == null)
									return new FoodSupply();
								return obj.Payload;
							}
							);
		if (supplies == null)
			return AwsHandlerResponse.Error;

		populationManager.Feed(supplies);
		return new AwsHandlerResponse(true, JsonSerializer.Serialize(supplies));
	}
}