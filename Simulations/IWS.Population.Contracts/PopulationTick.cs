using IWS.Contracts.Operations;
using IWS.Contracts.Time;

namespace IWS.Contracts.Population
{
	[Serializable]
	public class PopulationTick : Domain, Operation, Tickable
	{
		public PopulationTick() : base(Constants.DomainName)
		{
			Tick = Tick.Empty;
		}

		public string Name { get; set; } = "standard-tick";
		public Tick Tick { get; set; }
	}
}