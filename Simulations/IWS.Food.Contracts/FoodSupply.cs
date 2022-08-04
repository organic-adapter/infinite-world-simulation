using IWS.Contracts;

namespace IWS.Food.Contracts
{
	public class FoodSupply : Supply
	{
		public FoodSupply()
		{
			Type = typeof(FoodSupply).FullName ?? Type;
		}
	}
}