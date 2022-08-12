using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Amazon.SimpleNotificationService.Util;
using MessageQueues.Messages;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SNS.To.Lambda;

public class Function
{
	public async Task FunctionHandler(SNSEvent snsEvent, ILambdaContext context)
	{
		var access = new S3Access();

		foreach (SNSEvent.SNSRecord record in snsEvent.Records)
		{
			var bucketName = record.Sns.MessageAttributes["bucketName"].Value;

			await access.PutAsync(bucketName, Amazon.RegionEndpoint.USEast1, record);
		}
	}
}