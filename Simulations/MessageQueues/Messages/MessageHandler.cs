namespace MessageQueues.Messages
{
	public class MessageHandler
	{
		private readonly IEnumerable<MessageSourceHandler> messageSourceHandlers = new List<MessageSourceHandler>();

		public MessageHandler(IEnumerable<MessageSourceHandler> messageSourceHandlers)
		{
			this.messageSourceHandlers = messageSourceHandlers;
		}

		public void HandleAll<T>(Stream stream, Action<IEnumerable<T>> action)
			where T : class
		{
			var handler = FirstCompatibleHandler(stream);
			handler.HandleAll(stream, action);
		}

		public async Task HandleAllAsync<T>(Stream stream, Func<IEnumerable<T>, Task> action)
			where T : class
		{
			var handler = FirstCompatibleHandler(stream);
			await handler.HandleAllAsync(stream, action);
		}

		public async Task<IEnumerable<T>> HandleAllAsync<T>(Stream stream, Func<IEnumerable<T>, Task<IEnumerable<T>>> action)
			where T : class
		{
			var handler = FirstCompatibleHandler(stream);
			var response = await handler.HandleAllAsync(stream, action);
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