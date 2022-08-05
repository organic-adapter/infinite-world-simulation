namespace MessageQueues
{
	public abstract class MessageSubscriber
	{
		public abstract Stream Handler(Stream stream);
	}
}