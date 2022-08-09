namespace IWS.Contracts
{
	public interface LogItem : Unique
	{

	}

	public class LogItem<T> : LogItem
		where T : class
	{
		public LogItem()
		{
			Id = string.Empty;
			Timestamp = string.Empty;
		}

		public string Id { get; set; }
		public T? Payload { get; set; }
		public string Timestamp { get; set; }
	}
}