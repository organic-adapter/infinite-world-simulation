using IWS.Contracts.Operations;
using IWS.Contracts.Time;

namespace IWS.Contracts.Water
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