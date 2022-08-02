namespace MessageQueues.Messages
{
	[Serializable]
	public abstract class Message
	{
		public abstract T? GetPayload<T>() where T : class;
	}

	[Serializable]
	public abstract class Message<T> : Message
		where T : class
	{
		public DateTime? Timestamp { get; set; }
		public T? Payload { get; set; }

		public override TPub? GetPayload<TPub>()
			where TPub : class
		{
			return Payload as TPub;
		}
	}
}