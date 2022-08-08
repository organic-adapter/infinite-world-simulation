namespace Population.Business.Food
{
	public class FoodSupply
	{
		private const string DEFAULT = "Default";
		public FoodSupply()
		{
			Name = DEFAULT;
			Units = default;
		}

		public string Name { get; set; }
		public float Units { get; set; }
	}
}