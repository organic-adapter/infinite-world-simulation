using AEW.Contracts.Time;

namespace AEW.Events
{
	public class TickEnded : Event<Tick>
	{
		public TickEnded()
		{
		}

		public TickEnded(string sourceId, Tick tick)
		{
			Name = GetType().Name;
			SourceId = sourceId;
			Payload = tick;
		}
	}
}