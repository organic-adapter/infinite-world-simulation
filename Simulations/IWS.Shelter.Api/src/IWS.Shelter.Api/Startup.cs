using IWS.Common.Builders;
using IWS.Common.Startups;
using IWS.Contracts.Shelter;
using IWS.Shelter.Access;
using IWS.Shelter.Business;
using IWS.Shelter.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;
using static IWS.Common.DomainHierarchy;

namespace IWS.Shelter.Api
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			var domainName = Contracts.Shelter.Constants.DomainName;
			return ApiLambdaStartup
				.Services()
					.Configure<Common.Access.Aws.S3.AccessConfiguration<AwsS3ShelterAccess>>
						(options => { options.BucketName = "iws-shelter-proto-001"; })
					.AddAutoMapperWith(typeof(ShelterMappingProfiles))
					.AddAccessDefaults()
					.AddApiMessageHandler()
					.AddApiMessageSourceHandlerForType<ShelterTick>()
					.AddSingleton(DomainHierarchyBuilder.Build(domainName))
					.AddSingleton(new HierarchyTree(domainName))
					.AddSingleton<ShelterAccess, AwsS3ShelterAccess>()
					.AddSingleton<ShelterManager, AnatomyShelterManager>()
				.BuildServiceProvider();
		}
	}
}