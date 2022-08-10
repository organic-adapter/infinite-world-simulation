namespace AEW.Contracts
{
	public abstract class Domain : Unique
	{
		public Domain(string domainName)
		{
			DomainName = domainName;
			Id = string.Empty;
		}

		public string DomainName { get; set; } = string.Empty;
		public string Id { get; set; }
		public DomainOutput Output { get; set; } = new();
		public DomainRequirements Requirements { get; set; } = new();
	}
}