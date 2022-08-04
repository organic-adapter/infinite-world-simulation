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

		public abstract void Handle<T>(Stream stream, Action<T?> action)
			where T : class;

		public abstract Task HandleAsync<T>(Stream stream, Func<T?, Task> action)
			where T : class;

		public abstract bool IsCompatible(Stream stream);
	}

	public abstract class MessageSourceHandler<T> : MessageSourceHandler
		where T : class
	{
		public override Type MessageType => typeof(T);
		public T? Value { get; set; }

		public override void Handle<T1>(Stream stream, Action<T1?> action)
			where T1 : class
		{
			if (Value != null)
				Unpack(stream);

			action(Value as T1);
		}

		public override async Task HandleAsync<T1>(Stream stream, Func<T1?, Task> action)
					where T1 : class
		{
			if (Value != null)
				Unpack(stream);

			await action(Value as T1);
		}

		protected virtual UnpackResult Unpack(Stream stream)
		{
			using (var reader = new StreamReader(stream))
			{
				Json = reader.ReadToEnd();
				try
				{
					Value = JsonSerializer.Deserialize<T>(Json);
					stream.Position = 0;
					return UnpackResult.Ok(Value);
				}
				catch
				{
					stream.Position = 0;
					return UnpackResult.Error();
				}
			}
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