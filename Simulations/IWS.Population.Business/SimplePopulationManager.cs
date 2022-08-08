﻿using Food.POC.Resource;
using IWS.Contracts.Population;
using Population.Business.Food;
using System.Text.Json;

namespace IWS.Population.Business
{
	public class SimplePopulationManager : PopulationManager
	{
		private readonly FoodS3Resource resource;

		public SimplePopulationManager()
		{
			resource = new FoodS3Resource();
		}

		public void Feed(FoodSupply supply)
		{
			resource.SaveAsync(JsonSerializer.Serialize(supply)).Wait();
		}

		public void Feed(IEnumerable<FoodSupply> supplies)
		{
			foreach (var supply in supplies)
				Feed(supply);
		}

		public Task SaveAsync(PopulationTick populationTick)
		{
			throw new NotImplementedException();
		}
	}
}