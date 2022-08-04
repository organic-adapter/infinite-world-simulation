using IWS.Events;
using IWS.Food.Contracts;

namespace IWS.Food.Events
{
	public class FoodCreated : Event<FoodSupply>
	{
		public FoodCreated()
		{
			Name = GetType().Name;
		}
	}
}