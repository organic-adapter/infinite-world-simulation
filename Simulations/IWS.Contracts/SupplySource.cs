namespace IWS.Contracts
{
	[Serializable]
	public class SupplySource
	{
		public SupplySource()
		{
			SupplySourceId = string.Empty;
			SupplyType = string.Empty;
		}

		public float LastRequestFillRate { get; set; }
		public float RollingReliability { get; set; }
		public string SupplySourceId { get; set; }
		public string SupplyType { get; set; }
		public int WithdrawPriority { get; set; }
	}
}