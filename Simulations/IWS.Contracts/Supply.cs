namespace IWS.Contracts
{
	[Serializable]
	public class Supply
	{
		public static Supply Empty = new Supply();

		public Supply()
		{
			Type = string.Empty;
		}

		public int ExpirationTicks { get; set; }
		public float Quantity { get; set; }
		public string Type { get; set; }
	}
}