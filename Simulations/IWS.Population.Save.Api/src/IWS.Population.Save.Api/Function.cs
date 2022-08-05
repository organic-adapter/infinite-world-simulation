using IWS.Contracts.Population;
using IWS.Population.Business;
using MessageQueues.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace IWS.Population.Save.Api;

public class Function
{
	private readonly MessageHandler messageHandler;
	private readonly PopulationManager populationManager;

	public Function()
	{
		var provider = Startup.SetUp();
		populationManager = provider.GetService<PopulationManager>();
		messageHandler = provider.GetService<MessageHandler>();
	}

	public Stream FunctionHandler(Stream stream)
	{
		messageHandler.HandleAsync<PopulationTick>(stream, populationManager.SaveAsync).Wait();

		return stream;
	}

	public async Task Noop(PopulationTick? meh)
	{
		await Task.Run(()=> meh);
	}
}