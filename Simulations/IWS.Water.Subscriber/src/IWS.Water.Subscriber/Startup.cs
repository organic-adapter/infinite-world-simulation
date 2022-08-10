using IWS.Common.Builders;
using IWS.Common.Startups;
using IWS.Water.Access;
using IWS.Water.Business;
using IWS.Water.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;
using static IWS.Common.DomainHierarchy;

namespace IWS.Water.Subscriber
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			var coreSupplyBusConfiguration = AwsSnsStartup.GetCoreSupplyBusConfig();
			var domainName = Contracts.Water.Constants.DomainName;

			return ApiLambdaStartup
				.Services()
					.Configure(CoreDomainStartup.BuildAccessConfiguration<AwsS3WaterAccess>())
					.Configure(AwsSnsStartup.BuildOptions(coreSupplyBusConfiguration))
					.AddAutoMapperWith(typeof(WaterMappingProfiles))
					.AddAccessDefaults()
					.AddApiMessageHandler()
					.AddApiMessageSourceHandler<MessageQueues.Messages.SourceHandlers.SNSEventHandler>()
					.AddApiMessageSourceHandler<MessageQueues.Messages.SourceHandlers.SQSEventHandler>()
					.AddAwsSnsCoreSupplyBus()
					.AddSnsClient(coreSupplyBusConfiguration)
					.AddSingleton(DomainHierarchyBuilder.Build(domainName))
					.AddSingleton(new HierarchyTree(domainName))
					.AddSingleton<WaterAccess, AwsS3WaterAccess>()
					.AddSingleton<WaterManager, AnatomyWaterManager>()
				.BuildServiceProvider();
		}

	}
}