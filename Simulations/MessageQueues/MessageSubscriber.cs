namespace MessageQueues
{
	public abstract class MessageSubscriber
	{
		public abstract Stream FunctionHandler(Stream stream);
	}
}