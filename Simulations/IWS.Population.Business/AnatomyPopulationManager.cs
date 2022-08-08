using AutoMapper;
using IWS.Contracts.Population;
using IWS.Population.Access;
using Population.Business.Food;

namespace IWS.Population.Business
{
	public class AnatomyPopulationManager : PopulationManager
	{
		private readonly IMapper mapper;
		private readonly PopulationAccess populationAccess;

		public AnatomyPopulationManager
				(
					IMapper mapper,
					PopulationAccess populationAccess
				)
		{
			this.mapper = mapper;
			this.populationAccess = populationAccess;
		}

		public void Feed(FoodSupply supply)
		{
			throw new NotImplementedException();
		}

		public void Feed(IEnumerable<FoodSupply> supplies)
		{
			throw new NotImplementedException();
		}

		public async Task<PopulationTick> GetAsync(string id)
		{
			return await populationAccess.RetrieveAsync(id);
		}

		public async Task<PopulationTick> SaveAsync(PopulationTick populationTick)
		{
			var saveMe = mapper.Map<Access.Models.PopulationTick>(populationTick);
			//TODO: tick-history needs to be saved here as well.
			return await populationAccess.SaveAsync(saveMe);
		}
	}
}