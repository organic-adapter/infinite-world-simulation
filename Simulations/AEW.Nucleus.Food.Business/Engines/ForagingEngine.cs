using AEW.Common.Extensions;
using AEW.Contracts.Nucleus.Food;
using AEW.Nucleus.Food.Access;

namespace AEW.Nucleus.Food.Business.Engines
{
	public class ForagingEngine : FoodGeneratingEngine
	{
		private const float minimumRequirementDefault = 1000f;
		private readonly FoodAccess access;
		private readonly Dictionary<string, float> defaultRequirements;
		private readonly string domainName;
		private readonly float foragingDemandGrowthFloor = 1f;
		private readonly float foragingDemandGrowthPerTick = 1.005f;

		public ForagingEngine(FoodAccess access)
		{
			this.access = access;
			domainName = Constants.DomainName;
			defaultRequirements = new();
			defaultRequirements.Add(typeof(Contracts.Nucleus.Water.WaterSupply).AssemblyQualifiedName.ThrowIfNullOrEmpty(), minimumRequirementDefault);
		}

		public async Task<Dictionary<string, float>> SuppliesNeededAsync()
		{
			Access.Models.FoodTick lastTick = await access.GetLastTickAsync(domainName);
			return Grow(lastTick);
		}

		private Dictionary<string, float> Grow(FoodTick foodTick)
		{
			if (!foodTick.Requirements.Demands.Any())
				return defaultRequirements;
			var returnMe = new Dictionary<string, float>();

			foreach (var requirement in foodTick.Requirements.Demands)
			{
				var newQuantity = (float)Math.Max(foragingDemandGrowthFloor, requirement.Quantity * foragingDemandGrowthPerTick);
				returnMe.Add(requirement.SupplyType, newQuantity);
			}
			return returnMe;
		}
	}
}