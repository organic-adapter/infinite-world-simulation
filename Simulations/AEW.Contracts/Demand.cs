namespace AEW.Contracts
{
	[Serializable]
	public class Demand
	{
		public Demand()
		{
			DemandSourceId = string.Empty;
			SupplySourceId = string.Empty;
			SupplyType = string.Empty;
		}

		public float Quantity { get; set; }
		public string DemandSourceId { get; set; }
		public string SupplySourceId { get; set; }
		public string SupplyType { get; set; }
	}
}