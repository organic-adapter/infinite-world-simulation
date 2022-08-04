using IWS.Common.Startups;
using IWS.Contracts.Population;
using IWS.Population.Access;
using IWS.Population.Business;
using IWS.Population.Business.ModelMaps;
using Microsoft.Extensions.DependencyInjection;

namespace IWS.Population.Save.Api
{
	public static class Startup
	{
		public static IServiceProvider SetUp()
		{
			return ApiLambdaStartup
				.Services()
					.AddAutoMapperWith(typeof(PopulationMappingProfiles))
					.AddAccessDefaults()
					.AddApiMessageHandler()
					.AddApiMessageSourceHandlerForType<PopulationTick>()
					.AddSingleton<PopulationAccess, AwsS3PopulationAccess>()
					.AddSingleton<PopulationManager, AnatomyPopulationManager>()
				.BuildServiceProvider();
		}
	}
}