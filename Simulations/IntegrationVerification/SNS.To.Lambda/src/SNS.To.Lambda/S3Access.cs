using Amazon;
using Amazon.Lambda.SNSEvents;
using Amazon.S3;
using Amazon.S3.Model;
using System.Text.Json;

namespace SNS.To.Lambda
{
	public class S3Access
	{
		public S3Access()
		{
		}

		public async Task PutAsync(string bucketName, RegionEndpoint regionEndpoint, string message)
		{
			IAmazonS3 client = new AmazonS3Client(regionEndpoint);
			var putRequest = new PutObjectRequest()
			{
				BucketName = bucketName,
				ContentBody = message,
				Key = $"logs/rawmessage-{DateTime.Now.ToString("o")}.json"
			};

			await client.PutObjectAsync(putRequest);
		}

		public async Task PutAsync(string bucketName, RegionEndpoint regionEndpoint, SNSEvent.SNSRecord record)
		{
			IAmazonS3 client = new AmazonS3Client(regionEndpoint);
			var putRequest = new PutObjectRequest()
			{
				BucketName = bucketName,
				ContentBody = JsonSerializer.Serialize(record),
				Key = $"logs/rawRecord-{record.Sns.MessageId}.json"
			};

			await client.PutObjectAsync(putRequest);
		}
	}
}