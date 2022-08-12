using MessageQueues.Messages;

namespace AEW.Common.Access
{
	public interface NotificationBus
	{
		public Task PublishAsync<TMessage>(TMessage message)
			where TMessage : MessageBody;
	}
}