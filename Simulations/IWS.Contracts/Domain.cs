namespace IWS.Contracts
{
	public abstract class Domain
	{
		public Demand[] Demands { get; set; }
		public Domain[] Subdomains { get; set; }

		public SupplySource[] SupplySources { get; set; }

		public Supply[] TotalSupply { get; set; }

		public abstract Supply DispatchSupply(Demand demand);

		public abstract Supply GenerateSupply();

		public abstract void Tick();
	}
}