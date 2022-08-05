using MessageQueues.Messages;

namespace IWS.Events
{
	public class Event<T> : MessageBody<T>
		where T : class
	{
		public Event()
		{
			Name = string.Empty;
			SourceId = string.Empty;
		}

		public string Name { get; set; }
		public string SourceId { get; set; }
		public DateTime Timestamp { get; set; }
	}
}