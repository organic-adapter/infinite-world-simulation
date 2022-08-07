﻿using IWS.Common.Access.Aws.S3;
using MessageQueues.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace IWS.Common.Startups
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
			services.AddSingleton<FilePathBuilder, DefaultFilePathBuilder>();

			return _services;
		}

		public static IServiceCollection AddApiMessageHandler(this IServiceCollection services)
		{
			services.AddSingleton<MessageHandler>();
			return services;
		}
		public static IServiceCollection AddApiMessageSourceHandlerForString(this IServiceCollection services)
		{
			services.AddSingleton<MessageSourceHandler, MessageQueues.Messages.SourceHandlers.StringHandler<string>>();
			
			return services;
		}
		public static IServiceCollection AddApiMessageSourceHandlerForType<T>(this IServiceCollection services)
			where T : class
		{
			services.AddSingleton<MessageSourceHandler, MessageQueues.Messages.SourceHandlers.PocoHandler<T>>();

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