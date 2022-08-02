namespace Population.Business.Food
{
	public class FoodSupply
	{
		public FoodSupply()
		{
			Name = string.Empty;
			Units = default;
		}

		public string Name { get; set; }
		public float Units { get; set; }
	}
}