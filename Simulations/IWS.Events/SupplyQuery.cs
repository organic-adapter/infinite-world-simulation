namespace IWS.Events
{
	/// <summary>
	/// Used to ask domains that generate supplies if they produce a particular type.
	/// </summary>
	[Serializable]
	public class SupplyQuery
	{
		public SupplyQuery()
		{
			SupplyType = string.Empty;
		}

		/// <summary>
		/// A common contract domain type.
		/// </summary>
		public string SupplyType { get; set; }
	}
}