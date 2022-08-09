using System.Text.Json;

namespace MessageQueues.Messages
{
	public abstract class MessageSourceHandler
	{
		public MessageSourceHandler()
		{
			Json = string.Empty;
		}

		public string Json { get; set; }
		public abstract Type MessageType { get; }

		public abstract void HandleAll<T>(Stream stream, Action<IEnumerable<T>> action)
			where T : class;

		public abstract Task HandleAllAsync<T>(Stream stream, Func<IEnumerable<T>, Task> action)
			where T : class;

		public abstract Task<IEnumerable<T>> HandleAllAsync<T>(Stream stream, Func<IEnumerable<T>, Task<IEnumerable<T>>> action)
			where T : class;

		public abstract bool IsCompatible(Stream stream);
	}

	public abstract class MessageSourceHandler<T> : MessageSourceHandler
		where T : class
	{
		protected static T Empty = Activator.CreateInstance<T>();

		public override Type MessageType => typeof(T);

		public T UnpackedValue { get; set; } = Empty;

		public override void HandleAll<TMessage>(Stream stream, Action<IEnumerable<TMessage>> action)
									where TMessage : class
		{
			if (UnpackedValue == Empty)
				Unpack(stream);

			var messages = UnpackRecords<TMessage>(UnpackedValue);
			action(messages);
		}

		public override async Task HandleAllAsync<TMessage>(Stream stream, Func<IEnumerable<TMessage>, Task> action)
			where TMessage : class
		{
			if (UnpackedValue == Empty)
				Unpack(stream);

			var messages = UnpackRecords<TMessage>(UnpackedValue);
			await action(messages);
		}

		public override async Task<IEnumerable<TMessage>> HandleAllAsync<TMessage>(Stream stream, Func<IEnumerable<TMessage>, Task<IEnumerable<TMessage>>> action)
			where TMessage : class
		{
			if (UnpackedValue == Empty)
				Unpack(stream);

			var messages = UnpackRecords<TMessage>(UnpackedValue);
			return await action(messages);
		}

		protected virtual T? Deserialize(string json)
		{
			return JsonSerializer.Deserialize<T>(json);
		}

		protected virtual UnpackResult Unpack(Stream stream)
		{
			using (var reader = new StreamReader(stream))
			{
				Json = reader.ReadToEnd();
				try
				{
					UnpackedValue = JsonSerializer.Deserialize<T>(Json) ?? Empty;
					stream.Position = 0;
					return UnpackResult.Ok(UnpackedValue);
				}
				catch
				{
					stream.Position = 0;
					return UnpackResult.Error();
				}
			}
		}

		protected abstract IEnumerable<TMessage> UnpackRecords<TMessage>(T? message)
			where TMessage : class;

		public class MessageContainsPoisonPillRecord : Exception
		{
			public MessageContainsPoisonPillRecord(object messageObject)
			{
				MessageObject = messageObject;
			}

			public object? MessageObject { get; set; }
		}

		public class UnpackResult
		{
			public const bool FAILED = false;
			public const bool SUCCESSFUL = true;

			public UnpackResult()
			{
			}

			public UnpackResult(bool success, T? value)
			{
				Successful = success;
				Value = value;
			}

			public bool Successful { get; set; }
			public T? Value { get; set; }

			public static UnpackResult Error()
			{
				return new UnpackResult(FAILED, null);
			}

			public static UnpackResult Ok(T? value)
			{
				return new UnpackResult(SUCCESSFUL, value);
			}
		}
	}
}