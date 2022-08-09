using Amazon.Lambda.SNSEvents;
using System.Text.Json;
using static Amazon.Lambda.SNSEvents.SNSEvent;

namespace MessageQueues.Messages.SourceHandlers
{
	public class SNSEventHandler : MessageSourceHandler<SNSEvent>
	{
		public override bool IsCompatible(Stream stream)
		{
			var unpackedResult = Unpack(stream);
			return unpackedResult.Successful && AnyRecordsHaveRequiredMembers(unpackedResult);
		}

		protected override IEnumerable<TMessage> UnpackRecords<TMessage>(SNSEvent? message)
		{
			if (message == null)
				throw new ArgumentNullException();

			return message.Records.Select(record =>
				JsonSerializer.Deserialize<TMessage>(record.Sns.Message)
					?? throw new MessageContainsPoisonPillRecord(record)
			);
		}

		private static bool AnyRecordsHaveRequiredMembers(UnpackResult unpackResult)
		{
			return unpackResult.Value != null
				&& unpackResult.Value.Records.Any(record => RecordHasRequiredMembers(record));
		}

		private static bool RecordHasRequiredMembers(SNSRecord record)
		{
			return !string.IsNullOrEmpty(record.EventSource)
				&& !string.IsNullOrEmpty(record.Sns.Message)
				&& !string.IsNullOrEmpty(record.Sns.MessageId)
				&& !string.IsNullOrEmpty(record.Sns.Signature)
				&& !string.IsNullOrEmpty(record.Sns.SignatureVersion)
				&& record.Sns.Timestamp != new DateTime();
		}
	}
}