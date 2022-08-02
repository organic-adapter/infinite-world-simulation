namespace Food.Supply.POC.Contracts
{
	public class Supply
	{
		public Supply()
		{
			Generated = DateTime.Now;
			Name = string.Empty;
			Units = default;
		}

		public DateTime Generated { get; set; }
		public string Name { get; set; }
		public float Units { get; set; }
	}
}