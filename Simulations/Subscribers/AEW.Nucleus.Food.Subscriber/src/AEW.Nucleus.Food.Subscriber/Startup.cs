using AEW.Common.Builders;
using AEW.Common.Startups;
using AEW.Nucleus.Food.Access;
using AEW.Nucleus.Food.Business;
using AEW.Nucleus.Food.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;
using static AEW.Common.DomainHierarchy;

namespace AEW.Food.Subscriber
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			var coreSupplyBusConfiguration = AwsSnsStartup.GetCoreSupplyBusConfig();
			var domainName = Contracts.Nucleus.Food.Constants.DomainName;

			return ApiLambdaStartup
				.Services()
					.Configure(CoreDomainStartup.BuildAccessConfiguration<AwsS3FoodAccess>())
					.Configure(AwsSnsStartup.BuildOptions(coreSupplyBusConfiguration))
					.AddAutoMapperWith(typeof(FoodMappingProfiles))
					.AddAccessDefaults()
					.AddApiMessageHandler()
					.AddApiMessageSourceHandler<MessageQueues.Messages.SourceHandlers.SNSEventHandler>()
					.AddApiMessageSourceHandler<MessageQueues.Messages.SourceHandlers.SQSEventHandler>()
					.AddAwsSnsCoreSupplyBus()
					.AddSnsClient(coreSupplyBusConfiguration)
					.AddSingleton(DomainHierarchyBuilder.Build(domainName))
					.AddSingleton(new HierarchyTree(domainName))
					.AddSingleton<FoodAccess, AwsS3FoodAccess>()
					.AddSingleton<FoodManager, AnatomyFoodManager>()
				.BuildServiceProvider();
		}

	}
}