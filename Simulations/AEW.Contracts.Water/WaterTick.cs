using AEW.Contracts.Operations;
using AEW.Contracts.Time;

namespace AEW.Contracts.Nucleus.Water
{
	[Serializable]
	public class WaterTick : Domain, Operation, Tickable
	{
		public WaterTick() : base(Constants.DomainName)
		{
			Tick = Tick.Empty;
		}

		public string Name { get; set; } = "standard-tick";
		public Tick Tick { get; set; }
	}
}