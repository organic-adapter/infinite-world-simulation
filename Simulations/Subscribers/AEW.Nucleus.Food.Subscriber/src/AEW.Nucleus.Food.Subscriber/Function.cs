using AEW.Events.Nucleus.Food;
using AEW.Nucleus.Food.Business;
using AEW.Food.Subscriber;
using MessageQueues.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace AEW.Nucleus.Food.Subscriber
{
	public class Function
	{
		private readonly MessageHandler messageHandler;
		private readonly FoodManager foodManager;

		public Function() : this(Startup.SetUp())
		{
		}

		public Function(IServiceProvider provider)
		{
			foodManager = provider.GetService<FoodManager>()
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
			await messageHandler.HandleAllAsync<FoodDemanded>(stream, foodManager.ProcessDemands);

			return stream;
		}
	}
}