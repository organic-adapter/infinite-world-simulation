using AEW.Common.Access.Aws.S3;
using AEW.Common.Builders;
using MessageQueues.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace AEW.Common.Startups
{
	public static class ApiLambdaStartup
	{
		private static IServiceCollection _services;

		static ApiLambdaStartup()
		{
			_services = new ServiceCollection();
		}
		public static IServiceCollection AddAccessDefaults(this IServiceCollection services)
		{
			services.AddTransient<FilePathBuilder, DefaultFilePathBuilder>();

			return _services;
		}
		public static IServiceCollection AddAccessDefaults(this IServiceCollection services, string domainName)
		{
			services
				.AddSingleton(DomainHierarchyBuilder.Build(domainName))
				.AddTransient<FilePathBuilder, DefaultFilePathBuilder>();

			return _services;
		}

		public static IServiceCollection AddApiMessageHandler(this IServiceCollection services)
		{
			services.AddTransient<MessageHandler>();

			return services;
		}

		public static IServiceCollection AddApiMessageSourceHandler<T>(this IServiceCollection services)
			where T : MessageSourceHandler
		{
			services.AddTransient<MessageSourceHandler, T>();

			return services;
		}

		public static IServiceCollection AddAutoMapperWith(this IServiceCollection services, params Type[] types)
		{
			services.AddAutoMapper(types);
			return services;
		}

		public static IServiceProvider BuildProvider()
		{
			return _services.BuildServiceProvider();
		}

		public static IServiceCollection Services()
		{
			return _services;
		}
	}
}