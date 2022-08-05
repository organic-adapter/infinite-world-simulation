namespace MessageQueues.Messages.SourceHandlers
{
	public class AwsSnsHandler : MessageSourceHandler<AwsSnsMessage>
	{
		public override bool IsCompatible(Stream stream)
		{
			var unpackedResult = Unpack(stream);
			return unpackedResult.Successful && AnyRecordsHaveRequiredMembers(unpackedResult);
		}

		private static bool AnyRecordsHaveRequiredMembers(UnpackResult unpackResult)
		{
			return unpackResult.Value != null
				&& unpackResult.Value.Records.Any(record => RecordHasRequiredMembers(record));
		}

		private static bool RecordHasRequiredMembers(AwsSnsRecord record)
		{
			return !string.IsNullOrEmpty(record.EventSource)
				&& !string.IsNullOrEmpty(record.Sns.Message)
				&& !string.IsNullOrEmpty(record.Sns.MessageId)
				&& !string.IsNullOrEmpty(record.Sns.Timestamp)
				&& !string.IsNullOrEmpty(record.Sns.Signature)
				&& !string.IsNullOrEmpty(record.Sns.SignatureVersion);
		}

	}
}