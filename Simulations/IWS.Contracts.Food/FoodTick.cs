using IWS.Contracts.Operations;
using IWS.Contracts.Time;

namespace IWS.Contracts.Food
{
	[Serializable]
	public class FoodTick : Domain, Operation, Tickable
	{
		public FoodTick() : base(Constants.DomainName)
		{
			Tick = Tick.Empty;
		}

		public string Name { get; set; } = "standard-tick";
		public Tick Tick { get; set; }
	}
}