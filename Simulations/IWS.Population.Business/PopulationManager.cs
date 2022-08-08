﻿using IWS.Contracts.Population;
using Population.Business.Food;

namespace IWS.Population.Business
{
	public interface PopulationManager
	{
		public void Feed(FoodSupply supply);
		public void Feed(IEnumerable<FoodSupply> supplies);

		public Task SaveAsync(PopulationTick? populationTick);
	}
}