namespace AEW.Contracts
{
	[Serializable]
	public class DomainRelationship
	{
		public DomainRelationship()
		{
			Children = new();
			Focus = string.Empty;
		}

		public List<string> Children { get; set; }
		public string Focus { get; set; }
		public string? Parent { get; set; }
	}
}