using Amazon.Lambda.Core;
using IWS.Events.Water;
using IWS.Water.Business;
using MessageQueues;
using MessageQueues.Messages;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace IWS.Water.Subscriber;

public class Function
{
	private readonly MessageHandler messageHandler;
	private readonly WaterManager waterManager;

	public Function() : this(Startup.SetUp())
	{
	}

	public Function(IServiceProvider provider)
	{
		waterManager = provider.GetService<WaterManager>()
				?? throw new Exception("Service not found");
		messageHandler = provider.GetService<MessageHandler>()
						?? throw new Exception("Service not found");
	}

	public async Task<Stream> FunctionHandler(Stream stream)
	{
		await messageHandler.HandleAllAsync<WaterDemanded>(stream, waterManager.ProcessDemands);

		return stream;
	}
}
