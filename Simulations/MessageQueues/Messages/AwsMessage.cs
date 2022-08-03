namespace MessageQueues.Messages
{
	[Serializable]
	public class AwsHandlerResponse
	{
		public static AwsHandlerResponse Error = new AwsHandlerResponse() { Successful = false };
		public static AwsHandlerResponse Ok = new AwsHandlerResponse() { Successful = true };

		public AwsHandlerResponse()
		{
			Message = string.Empty;
		}

		public AwsHandlerResponse(bool successful, string message)
		{
			Successful = successful;
			Message = message;
		}

		public string Message { get; set; }
		public bool Successful { get; set; }
	}

	[Serializable]
	public class AwsSns
	{
		public AwsSns()
		{
			Message = string.Empty;
			MessageAttributes = new();
			MessageId = string.Empty;
			Signature = string.Empty;
			SignatureVersion = string.Empty;
			SigningCertUrl = string.Empty;
			Timestamp = string.Empty;
			TopicArn = string.Empty;
			Type = string.Empty;
			UnsubscribeUrl = string.Empty;
		}

		public string Message { get; set; }
		public Dictionary<string, string> MessageAttributes { get; set; }
		public string MessageId { get; set; }
		public string Signature { get; set; }
		public string SignatureVersion { get; set; }
		public string SigningCertUrl { get; set; }
		public string? Subject { get; set; }
		public string Timestamp { get; set; }
		public string TopicArn { get; set; }
		public string Type { get; set; }
		public string UnsubscribeUrl { get; set; }
	}

	[Serializable]
	public class AwsSnsMessage
	{
		public AwsSnsMessage()
		{
			Records = new List<AwsSnsRecord>();
		}

		public List<AwsSnsRecord> Records { get; set; }
	}

	[Serializable]
	public class AwsSnsRecord
	{
		public string EventSource { get; set; }
		public string EventSubscriptionArn { get; set; }
		public string EventVersion { get; set; }
		public AwsSns Sns { get; set; }
	}

	[Serializable]
	public class AwsSqsMessage
	{
		public AwsSqsMessage()
		{
			Records = new List<AwsSqsRecord>();
		}

		public List<AwsSqsRecord> Records { get; set; }
	}

	[Serializable]
	public class AwsSqsRecord
	{
		public AwsSqsRecord()
		{
			AwsRegion = string.Empty;
			Attributes = new();
			Body = string.Empty;
			MessageAttributes = new();
			EventSource = string.Empty;
			EventSourceARN = string.Empty;
			Md5OfBody = string.Empty;
			MessageId = string.Empty;
			ReceiptHandle = string.Empty;
		}

		public Dictionary<string, string> Attributes { get; set; }
		public string AwsRegion { get; set; }
		public string Body { get; set; }
		public string EventSource { get; set; }
		public string EventSourceARN { get; set; }
		public string Md5OfBody { get; set; }
		public Dictionary<string, string> MessageAttributes { get; set; }
		public string MessageId { get; set; }
		public string ReceiptHandle { get; set; }
	}
}