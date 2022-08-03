using Amazon.Lambda.Core;
using MessageQueues.Messages;

namespace MessageQueues
{
	public abstract class SubscriberAWS
	{
		public abstract AwsHandlerResponse FunctionHandler(AwsSnsMessage message, ILambdaContext context);
	}
}