using AEW.Nucleus.Food.Access;
using AEW.Nucleus.Food.Business.Engines;
using Moq;

namespace AEW.Nucleus.Food.Business.Tests
{
	public class ForagingEngineTests
	{
		public class WhenItReceivesATick
		{
			private readonly Mock<FoodAccess> foodAccessMock;

			public WhenItReceivesATick()
			{
				foodAccessMock = new Mock<FoodAccess>();
			}

			[Fact]
			public async Task It_Defaults_SupplyRequirements_If_The_Simulation_Is_New()
			{
				foodAccessMock.Setup(mock => mock.GetLastTickAsync(It.IsAny<string>()))
					.ReturnsAsync(new Access.Models.FoodTick());
				FoodGeneratingEngine engine = new ForagingEngine(foodAccessMock.Object);

				var supplies = await engine.SuppliesNeededAsync();

				Assert.True(supplies.Any());
			}

			[Fact]
			public async Task It_Grows_SupplyRequirements_For_Existing_Simulations()
			{
				var supplyType = "Unit Test Demand";
				var quantity = 3337f;
				var lastTick = new Access.Models.FoodTick();
				lastTick.Requirements.Demands.Add(new Contracts.Demand() { Quantity = quantity, SupplyType = supplyType });
				foodAccessMock.Setup(mock => mock.GetLastTickAsync(It.IsAny<string>()))
					.ReturnsAsync(lastTick);
				FoodGeneratingEngine engine = new ForagingEngine(foodAccessMock.Object);

				var supplies = await engine.SuppliesNeededAsync();

				Assert.True(supplies.Any());
				Assert.True(supplies[supplyType] > quantity);
			}
		}
	}
}