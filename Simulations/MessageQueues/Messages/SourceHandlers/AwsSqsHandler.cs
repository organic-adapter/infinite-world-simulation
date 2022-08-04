﻿namespace MessageQueues.Messages.SourceHandlers
{
	public class AwsSqsHandler : MessageSourceHandler<AwsSqsMessage>
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

		private static bool RecordHasRequiredMembers(AwsSqsRecord record)
		{
			return !string.IsNullOrEmpty(record.Body)
				&& !string.IsNullOrEmpty(record.EventSource)
				&& !string.IsNullOrEmpty(record.MessageId)
				&& !string.IsNullOrEmpty(record.Md5OfBody);
		}
	}
}