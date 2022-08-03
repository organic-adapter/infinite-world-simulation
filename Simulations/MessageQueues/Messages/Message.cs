namespace MessageQueues.Messages
{

	[Serializable]
	public class MessageBody<T>
		where T : class
	{
		public string FQN => typeof(T).FullName ?? string.Empty;
		public T? Payload { get; set; }
	}
}