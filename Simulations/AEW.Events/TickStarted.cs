using AEW.Contracts.Time;

namespace AEW.Events
{
	public class TickStarted : Event<Tick>
	{
		public TickStarted()
		{
		}
		public TickStarted(string sourceId, Tick tick)
		{
			Name = GetType().Name;
			SourceId = sourceId;
			Payload = tick;
		}
	}
}