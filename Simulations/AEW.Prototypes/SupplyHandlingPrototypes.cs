using AEW.Events;

namespace AEW.Prototypes
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
				Quantity = demand.Payload?.Quantity ?? default,
				Type = demand.Payload?.SupplyType ?? string.Empty,
			};

			return returnMe;
		}
		public static TOut AlwaysDispatchHalfOfWhatIsDemanded<TIn, TOut>(this TIn demand)
			where TIn : SupplyDemanded
			where TOut : SupplyDispatched
		{
			var returnMe = Activator.CreateInstance<TOut>();
			returnMe.Payload = new Contracts.Supply()
			{
				Quantity = (demand.Payload?.Quantity ?? default) / 2,
				Type = demand.Payload?.SupplyType ?? string.Empty,
			};

			return returnMe;
		}
	}
}