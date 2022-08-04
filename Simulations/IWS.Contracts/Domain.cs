namespace IWS.Contracts
{
	public abstract class Domain
	{
		public Domain(string domainName)
		{
			DomainName = domainName;
		}
		public string DomainName { get; set; } = string.Empty;
		public DomainOutput Output { get; set; } = new();
		public DomainRequirements Requirements { get; set; } = new();
	}
}