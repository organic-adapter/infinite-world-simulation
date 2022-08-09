using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Options;

namespace IWS.Common.Access.Aws.Sns
{
	public class CoreSupplyBus : BaseBus<CoreSupplyBus>, Access.CoreSupplyBus
	{
		public CoreSupplyBus(AmazonSimpleNotificationServiceClient client, IOptionsMonitor<BusConfiguration<CoreSupplyBus>> options) : base(client, options)
		{
		}
	}
}