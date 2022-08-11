using AEW.Events.Nucleus.Water;
using AEW.Nucleus.Water.Business;
using AEW.Water.Subscriber;
using MessageQueues.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace AEW.Nucleus.Water.Subscriber
{
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

		/// <summary>
		/// A simple function that takes a string and does a ToUpper
		/// </summary>
		/// <param name="input"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task<Stream> FunctionHandler(Stream stream)
		{
			await messageHandler.HandleAllAsync<WaterDemanded>(stream, waterManager.ProcessDemands);

			return stream;
		}
	}
}