namespace AEW.Contracts
{
	public class DomainRequirements
	{
		public DomainRequirements()
		{
			Demands = new();
			SupplySources = new();
		}

		public List<Demand> Demands { get; set; }
		public List<SupplySource> SupplySources { get; set; }
	}
}