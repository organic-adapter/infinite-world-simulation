namespace MessageQueues.Messages.SourceHandlers
{
	public class IwsPayloadHandler : MessageSourceHandler<MessageBody>
	{
		public override void Handle<T>(Stream stream, Action<T?> action)
			where T : class
		{
			if (Value != null)
				Unpack(stream);

			action(Value as T);
		}

		public override bool IsCompatible(Stream stream)
		{
			var unpackedResult = Unpack(stream);
			return unpackedResult.Successful && RecordHasRequiredMembers(unpackedResult.Value);
		}

		private static bool RecordHasRequiredMembers(MessageBody? record)
		{
			return record != null
				&& !string.IsNullOrEmpty(record.FQN)
				&& record.Body != null;
		}
	}
}