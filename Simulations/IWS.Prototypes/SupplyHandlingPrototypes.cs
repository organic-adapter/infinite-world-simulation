using IWS.Events;

namespace IWS.Prototypes
{
	public static class SupplyHandlingPrototypes
	{
		public static TOut AlwaysDispatchWhatIsDemanded<TIn, TOut>(this TIn demand)
			where TIn : SupplyDemanded
			where TOut : SupplyDispatched
		{
			var returnMe = Activator.CreateInstance<TOut>();
			returnMe.Payload = new Contracts.Supply()
			{
				Quantity = demand.Payload.Quantity,
				Type = demand.Payload.SupplyType,
			};

			return returnMe;
		}
	}
}