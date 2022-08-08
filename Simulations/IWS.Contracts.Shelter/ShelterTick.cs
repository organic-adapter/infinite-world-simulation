using IWS.Contracts.Operations;
using IWS.Contracts.Time;

namespace IWS.Contracts.Shelter
{
	[Serializable]
	public class ShelterTick : Domain, Operation, Tickable
	{
		public ShelterTick() : base(Constants.DomainName)
		{
			Tick = Tick.Empty;
		}

		public string Name { get; set; } = "standard-tick";
		public Tick Tick { get; set; }
	}
}