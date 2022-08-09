using Amazon.SimpleNotificationService;
using IWS.Common.Access;
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

		public static IServiceCollection AddSnsConfig<T>(this IServiceCollection services, Access.Aws.Sns.BusConfiguration<T> config)
					where T : NotificationBus
		{
			services.AddSingleton(new AmazonSimpleNotificationServiceClient(config.RegionEndpoint));

			return services;
		}
	}
}