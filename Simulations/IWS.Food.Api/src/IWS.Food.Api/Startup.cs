using IWS.Common.Builders;
using IWS.Common.Startups;
using IWS.Contracts.Food;
using IWS.Food.Access;
using IWS.Food.Business;
using IWS.Food.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;
using static IWS.Common.DomainHierarchy;

namespace IWS.Food.Api
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			var domainName = Contracts.Food.Constants.DomainName;
			return ApiLambdaStartup
				.Services()
					.Configure<Common.Access.Aws.S3.AccessConfiguration<AwsS3FoodAccess>>
						(options => { options.BucketName = "iws-food-proto-001"; })
					.AddAutoMapperWith(typeof(FoodMappingProfiles))
					.AddAccessDefaults()
					.AddApiMessageHandler()
					.AddApiMessageSourceHandlerForType<FoodTick>()
					.AddSingleton(DomainHierarchyBuilder.Build(domainName))
					.AddSingleton(new HierarchyTree(domainName))
					.AddSingleton<FoodAccess, AwsS3FoodAccess>()
					.AddSingleton<FoodManager, AnatomyFoodManager>()
				.BuildServiceProvider();
		}
	}
}