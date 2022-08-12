using Amazon.SimpleNotificationService;
using AEW.Common.Access;
using AEW.Common.Startups.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace AEW.Common.Startups
{
	public static class AwsSnsStartup
	{
		public static IServiceCollection AddAwsSnsCoreSupplyBus(this IServiceCollection services)
		{
			services.AddSingleton<Access.NucleusSupplyBus, Access.Aws.Sns.NucleusSupplyBus>();

			return services;
		}

		public static IServiceCollection AddSnsClient<T>(this IServiceCollection services, Access.Aws.Sns.BusConfiguration<T> config)
					where T : NotificationBus
		{
			services.AddSingleton(new AmazonSimpleNotificationServiceClient(config.RegionEndpoint));

			return services;
		}

		public static Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.NucleusSupplyBus> GetCoreSupplyBusConfig()
		{
			var coreSupplyBusConfiguration = new Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.NucleusSupplyBus>()
			{
				RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("NUCLEUS_SUPPLY_TOPIC__REGION")),
				TopicArn = Environment.GetEnvironmentVariable("NUCLEUS_SUPPLY_TOPIC__ARN") ?? throw new MissingEnvironmentVariable()
			};
			return coreSupplyBusConfiguration;
		}


		public static Action<Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.NucleusSupplyBus>> BuildOptions(Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.NucleusSupplyBus> config)
		{
			return (Common.Access.Aws.Sns.BusConfiguration<Common.Access.Aws.Sns.NucleusSupplyBus> options) =>
			{
				options.TopicArn = config.TopicArn;
				options.RegionEndpoint = config.RegionEndpoint;
			};
		}
	}
}