namespace AEW.Contracts
{
	public class DomainOutput
	{
		public DomainOutput()
		{
			Subdomains = new();
			TotalSupply = new();
		}

		public List<Domain> Subdomains { get; set; }

		public List<Supply> TotalSupply { get; set; }
	}
}