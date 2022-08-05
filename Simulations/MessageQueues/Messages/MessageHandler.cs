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

		public async Task HandleAsync<T>(Stream stream, Func<T?, Task> action)
			where T : class
		{
			var handler = FirstCompatibleHandler(stream);
			await handler.HandleAsync(stream, action);
		}

		public async Task<T> HandleAsync<T>(Stream stream, Func<T?, Task<T>> action)
			where T : class
		{
			var handler = FirstCompatibleHandler(stream);
			var response = await handler.HandleAsync(stream, action);
			return response;
		}

		public async Task<T> HandleAsync<T>(Stream stream, Func<string?, Task<T>> action)
			where T : class
		{
			var handler = FirstCompatibleHandler(stream);
			var response = await handler.HandleAsync(stream, action);
			return response;
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