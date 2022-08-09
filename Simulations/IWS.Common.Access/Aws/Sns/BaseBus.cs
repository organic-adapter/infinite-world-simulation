using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MessageQueues.Messages;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace IWS.Common.Access.Aws.Sns
{
	public class BaseBus<TOptions> : NotificationBus
		where TOptions : NotificationBus
	{
		private readonly AmazonSimpleNotificationServiceClient client;
		private readonly IOptionsMonitor<BusConfiguration<TOptions>> options;

		public BaseBus(AmazonSimpleNotificationServiceClient client, IOptionsMonitor<BusConfiguration<TOptions>> options)
		{
			this.client = client;
			this.options = options;
		}

		public async Task PublishAsync<TMessage>(TMessage message)
			where TMessage : MessageBody
		{
			var request = GeneratePublishRequest(message);
			var response = await client.PublishAsync(request);
		}

		private PublishRequest GeneratePublishRequest<T>(T message)
			where T : class
		{
			var json = JsonSerializer.Serialize(message);
			var attributes = new Dictionary<string, MessageAttributeValue>()
					{
						{ "messageType", new MessageAttributeValue(){ StringValue = typeof(T).Name, DataType = "String" } },
						{ "messageFullType", new MessageAttributeValue(){ StringValue = typeof(T).FullName, DataType = "String" } },
						{ "reportType", new MessageAttributeValue(){ StringValue = typeof(T).Name, DataType = "String" } },
					};

			return new PublishRequest()
			{
				Message = json,
				TopicArn = options.CurrentValue.TopicArn,
				MessageAttributes = attributes,
			};
		}
	}
}