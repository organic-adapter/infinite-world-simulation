using Amazon.Lambda.SQSEvents;
using System.Text.Json;
using static Amazon.Lambda.SQSEvents.SQSEvent;

namespace MessageQueues.Messages.SourceHandlers
{
	public class SQSEventHandler : MessageSourceHandler<SQSEvent>
	{
		public override bool IsCompatible(Stream stream)
		{
			var unpackedResult = Unpack(stream);
			return unpackedResult.Successful && AnyRecordsHaveRequiredMembers(unpackedResult);
		}

		protected override IEnumerable<TMessage> UnpackRecords<TMessage>(SQSEvent? message)
		{
			if (message == null)
				throw new ArgumentNullException();

			return message.Records.Select(record =>
				JsonSerializer.Deserialize<TMessage>(record.Body)
					?? throw new MessageContainsPoisonPillRecord(record)
			);
		}

		private static bool AnyRecordsHaveRequiredMembers(UnpackResult unpackResult)
		{
			return unpackResult.Value != null
				&& unpackResult.Value.Records.Any(record => RecordHasRequiredMembers(record));
		}

		private static bool RecordHasRequiredMembers(SQSMessage record)
		{
			return !string.IsNullOrEmpty(record.Body)
				&& !string.IsNullOrEmpty(record.EventSource)
				&& !string.IsNullOrEmpty(record.MessageId)
				&& !string.IsNullOrEmpty(record.Md5OfBody);
		}
	}
}