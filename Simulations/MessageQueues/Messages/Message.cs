namespace MessageQueues.Messages
{
	[Serializable]
	public class MessageBody
	{
		public MessageBody()
		{
			FQN = GetType().FullName ?? string.Empty;
		}

		public virtual object? Body { get; }
		public virtual string FQN { get; }
	}

	[Serializable]
	public class MessageBody<T> : MessageBody
		where T : class
	{
		public override object? Body => Payload;
		public override string FQN => typeof(T).FullName ?? string.Empty;
		public T? Payload { get; set; }
	}
}