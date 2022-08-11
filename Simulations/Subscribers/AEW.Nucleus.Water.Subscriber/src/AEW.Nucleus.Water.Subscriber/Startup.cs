using AEW.Common.Builders;
using AEW.Common.Startups;
using AEW.Nucleus.Water.Access;
using AEW.Nucleus.Water.Business;
using AEW.Nucleus.Water.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;
using static AEW.Common.DomainHierarchy;

namespace AEW.Water.Subscriber
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			var coreSupplyBusConfiguration = AwsSnsStartup.GetCoreSupplyBusConfig();
			var domainName = Contracts.Nucleus.Water.Constants.DomainName;

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