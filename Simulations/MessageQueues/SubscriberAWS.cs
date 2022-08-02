using Amazon.Lambda.Core;
using MessageQueues.Messages;

namespace MessageQueues
{
	public abstract class SubscriberAWS<TIn, TOut> : Subscriber<TIn, TOut>
		where TIn : Message
		where TOut : class
	{
		public virtual TOut? FunctionHandler(TIn message, ILambdaContext context)
		{
			return Handle(message);
		}
	}
}