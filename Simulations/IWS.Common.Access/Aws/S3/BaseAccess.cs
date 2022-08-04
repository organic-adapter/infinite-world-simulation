using Amazon.S3;
using Amazon.S3.Model;
using IWS.Contracts;
using IWS.Contracts.Time;
using System.Text.Json;

namespace IWS.Common.Access.Aws.S3
{
	public abstract class BaseAccess : Access
	{
		protected readonly string bucketName;
		protected readonly FilePathBuilder filePathBuilder;

		public BaseAccess(string bucketName, FilePathBuilder filePathBuilder)
		{
			this.bucketName = bucketName;
			this.filePathBuilder = filePathBuilder;
		}
		protected PutObjectRequest GenerateBaseRequest<T>(T putMe)
			where T : DefinedByName
		{
			var body = JsonSerializer.Serialize(putMe);
			var putRequest = new PutObjectRequest()
			{
				BucketName = bucketName,
				ContentBody = body,
				ContentType = "application/json",
			};

			return putRequest;
		}
		protected PutObjectRequest GeneratePutRequest<T>(T putMe)
			where T : DefinedByName
		{
			var putRequest = GenerateBaseRequest<T>(putMe);
			putRequest.FilePath = filePathBuilder.GetFilePath(putMe);
			
			return putRequest;
		}
		protected PutObjectRequest GeneratePutRequest<T>(T putMe, Tick tick)
			where T : DefinedByName
		{
			var putRequest = GenerateBaseRequest<T>(putMe);
			putRequest.FilePath = filePathBuilder.GetFilePath(putMe, tick);

			return putRequest;
		}
		protected async Task PutAsync<T>(T putMe, Tick tick)
			where T : DefinedByName
		{
			var putRequest = GeneratePutRequest<T>(putMe, tick);

			try
			{
				IAmazonS3 client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
				PutObjectResponse response = await client.PutObjectAsync(putRequest);
			}
			catch (AmazonS3Exception amazonS3Exception)
			{
				if (amazonS3Exception.ErrorCode != null &&
					(amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
					||
					amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
				{
					throw new Exception("Check the provided AWS Credentials.");
				}
				else
				{
					throw new Exception("Error occurred: " + amazonS3Exception.Message);
				}
			}
		}
	}
}