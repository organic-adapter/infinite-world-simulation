using Amazon.Lambda.Core;
using IWS.Contracts.Water;
using IWS.Water.Business;
using MessageQueues.Messages;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace IWS.Water.Get.Api;

public class Function
{

	private readonly MessageHandler messageHandler;
	private readonly WaterManager waterManager;

	public Function()
	{
		var provider = Startup.SetUp();
		waterManager = provider.GetService<WaterManager>();
		messageHandler = provider.GetService<MessageHandler>();
	}

	public async Task<WaterTick> FunctionHandler(Stream stream)
	{
		var response = await messageHandler.HandleAsync(stream, waterManager.GetAsync);

		return response;
	}

	public async Task Noop(WaterTick? meh)
	{
		await Task.Run(() => meh);
	}

	private Stream WriteToStream<T>(T writeMe)
	{
		var memoryStream = new MemoryStream();
		var json = JsonSerializer.Serialize(writeMe);
		memoryStream.Write(Encoding.UTF8.GetBytes(json));
		return memoryStream;
	}
}
