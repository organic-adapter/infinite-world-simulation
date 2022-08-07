using IWS.Common.Builders;
using IWS.Common.Startups;
using IWS.Contracts.Water;
using IWS.Water.Access;
using IWS.Water.Business;
using IWS.Water.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;
using static IWS.Common.DomainHierarchy;

namespace IWS.Water.Api
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			var domainName = Contracts.Water.Constants.DomainName;
			return ApiLambdaStartup
				.Services()
					.Configure<Common.Access.Aws.S3.AccessConfiguration<AwsS3WaterAccess>>
						(options => { options.BucketName = "iws-water-proto-001"; })
					.AddAutoMapperWith(typeof(WaterMappingProfiles))
					.AddAccessDefaults()
					.AddApiMessageHandler()
					.AddApiMessageSourceHandlerForType<WaterTick>()
					.AddSingleton(DomainHierarchyBuilder.Build(domainName))
					.AddSingleton(new HierarchyTree(domainName))
					.AddSingleton<WaterAccess, AwsS3WaterAccess>()
					.AddSingleton<WaterManager, AnatomyWaterManager>()
				.BuildServiceProvider();
		}
	}
}