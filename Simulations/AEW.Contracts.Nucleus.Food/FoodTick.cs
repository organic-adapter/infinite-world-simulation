using AEW.Contracts.Operations;
using AEW.Contracts.Time;

namespace AEW.Contracts.Nucleus.Food
{
	[Serializable]
	public class FoodTick : Domain, Operation, Tickable
	{
		public static FoodTick Empty = new FoodTick();

		public FoodTick() : base(Constants.DomainName)
		{
			Tick = Tick.Empty;
		}

		public string Name { get; set; } = "standard-tick";
		public Tick Tick { get; set; }
	}
}