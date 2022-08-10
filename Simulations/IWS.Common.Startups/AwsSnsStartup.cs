using Amazon.SimpleNotificationService;
using IWS.Common.Access;
using IWS.Common.Startups.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace IWS.Common.Startups
{
	public static class AwsSnsStartup
	{
		public static IServiceCollection AddAwsSnsCoreSupplyBus(this IServiceCollection services)
		{
			services.AddSingleton<Access.CoreSupplyBus, Access.Aws.Sns.CoreSupplyBus>();

			return services;
		}

		public static IServiceCollection AddSnsClient<T>(this IServiceCollection services, Access.Aws.Sns.BusConfiguration<T> config)
					where T : NotificationBus
		{
			services.AddSingleton(new AmazonSimpleNotificationServiceClient(config.RegionEndpoint));

			return services;
		}

		public static Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus> GetCoreSupplyBusConfig()
		{
			var coreSupplyBusConfiguration = new Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus>()
			{
				RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("CORE_SUPPLY_TOPIC__REGION")),
				TopicArn = Environment.GetEnvironmentVariable("CORE_SUPPLY_TOPIC__ARN") ?? throw new MissingEnvironmentVariable()
			};
			return coreSupplyBusConfiguration;
		}


		public static Action<Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus>> BuildOptions(Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus> config)
		{
			return (Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.CoreSupplyBus> options) =>
			{
				options.TopicArn = config.TopicArn;
				options.RegionEndpoint = config.RegionEndpoint;
			};
		}
	}
}