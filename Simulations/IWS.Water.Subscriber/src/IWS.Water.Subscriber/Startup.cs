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
			var coreSupplyBusConfiguration = new Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus>()
			{
				RegionEndpoint = Amazon.RegionEndpoint.USEast1
		,
				TopicArn = "arn:aws:sns:us-east-1:686681529839:iws-core-supply-proto-001"
			};

			var domainName = Contracts.Water.Constants.DomainName;

			return ApiLambdaStartup
				.Services()
					.Configure<Common.Access.Aws.S3.AccessConfiguration<AwsS3WaterAccess>>
						(options => { options.BucketName = "iws-water-proto-001"; })
					.Configure(BuildOptions(coreSupplyBusConfiguration))
					.AddAutoMapperWith(typeof(WaterMappingProfiles))
					.AddAccessDefaults()
					.AddApiMessageHandler()
					.AddApiMessageSourceHandler<MessageQueues.Messages.SourceHandlers.SNSEventHandler>()
					.AddApiMessageSourceHandler<MessageQueues.Messages.SourceHandlers.SQSEventHandler>()
					.AddAwsSnsCoreSupplyBus()
					.AddSnsConfig(coreSupplyBusConfiguration)
					.AddSingleton(DomainHierarchyBuilder.Build(domainName))
					.AddSingleton(new HierarchyTree(domainName))
					.AddSingleton<WaterAccess, AwsS3WaterAccess>()
					.AddSingleton<WaterManager, AnatomyWaterManager>()
				.BuildServiceProvider();
		}

		private static Action<Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus>> BuildOptions(Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus> config)
		{
			return (Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus> options) =>
			{
				options.TopicArn = config.TopicArn;
				options.RegionEndpoint = config.RegionEndpoint;
			};
		}
	}
}