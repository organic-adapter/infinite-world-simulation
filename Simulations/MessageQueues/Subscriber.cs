using MessageQueues.Messages;

namespace MessageQueues
{
	public abstract class Subscriber
	{
	}

	public abstract class Subscriber<TIn, TOut>
		where TIn : Message
		where TOut : class
	{
		public abstract TOut? Handle(TIn message);
	}
}