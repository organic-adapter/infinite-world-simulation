using IWS.Common.Builders;
using IWS.Common.Startups;
using IWS.Contracts.Population;
using IWS.Population.Access;
using IWS.Population.Business;
using IWS.Population.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;
using static IWS.Common.DomainHierarchy;

namespace IWS.Population.Api
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			var domainName = Contracts.Population.Constants.DomainName;
			return ApiLambdaStartup
				.Services()
					.Configure<Common.Access.Aws.S3.AccessConfiguration<AwsS3PopulationAccess>>
						(options => { options.BucketName = "iws-population-proto-001"; })
					.AddAutoMapperWith(typeof(PopulationMappingProfiles))
					.AddAccessDefaults()
					.AddSingleton(DomainHierarchyBuilder.Build(domainName))
					.AddSingleton(new HierarchyTree(domainName))
					.AddSingleton<PopulationAccess, AwsS3PopulationAccess>()
					.AddSingleton<PopulationManager, AnatomyPopulationManager>()
				.BuildServiceProvider();
		}
	}
}