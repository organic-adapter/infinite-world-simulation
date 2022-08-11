using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Options;

namespace AEW.Common.Access.Aws.Sns
{
	public class NucleusSupplyBus : BaseBus<NucleusSupplyBus>, Access.NucleusSupplyBus
	{
		public NucleusSupplyBus(AmazonSimpleNotificationServiceClient client, IOptionsMonitor<BusConfiguration<NucleusSupplyBus>> options) : base(client, options)
		{
		}
	}
}