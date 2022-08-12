using MessageQueues.Messages;

namespace IWS.Common.Access
{
	public interface NotificationBus
	{
		public Task PublishAsync<TMessage>(TMessage message)
			where TMessage : MessageBody;
	}
}