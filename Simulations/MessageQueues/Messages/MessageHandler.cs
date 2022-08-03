namespace MessageQueues.Messages
{
	public class MessageHandler
	{
		private readonly IEnumerable<MessageSourceHandler> messageSourceHandlers = new List<MessageSourceHandler>();

		public MessageHandler(IEnumerable<MessageSourceHandler> messageSourceHandlers)
		{
			this.messageSourceHandlers = messageSourceHandlers;
		}

		public void Handle<T>(Stream stream, Action<T?> action)
			where T : class
		{
			var handler = FirstCompatibleHandler(stream);
			handler.Handle(stream, action);
		}

		private MessageSourceHandler FirstCompatibleHandler(Stream stream)
		{
			foreach (var handler in messageSourceHandlers)
				if (handler.IsCompatible(stream))
					return handler;

			throw new NoCompatibleMessageSourceHandlers(stream);
		}

		public class NoCompatibleMessageSourceHandlers : Exception
		{
			public NoCompatibleMessageSourceHandlers(Stream stream)
			{
				Stream = stream;
			}

			public Stream Stream { get; private set; }
		}
	}
}