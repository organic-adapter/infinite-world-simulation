using Amazon.S3;
using Amazon.S3.Model;
using IWS.Contracts;
using IWS.Contracts.Time;
using System.Text.Json;

namespace IWS.Common.Access.Aws.S3
{
	public abstract class BaseAccess : ResourceAccess
	{
		protected readonly string bucketName;
		protected readonly FilePathBuilder filePathBuilder;

		public BaseAccess(string bucketName, FilePathBuilder filePathBuilder)
		{
			this.bucketName = bucketName;
			this.filePathBuilder = filePathBuilder;
		}

		protected GetObjectRequest GenerateBaseGetRequest(string id)
		{
			var getRequest = new GetObjectRequest()
			{
				BucketName = bucketName,
				Key = id,
			};
			return getRequest;
		}

		protected PutObjectRequest GenerateBasePutRequest<T>(T putMe, string filePath)
			where T : DefinedByName, Unique
		{
			var obj = putMe;
			obj = SetIdIfMissing(obj, filePath);
			var body = JsonSerializer.Serialize(obj);
			var putRequest = new PutObjectRequest()
			{
				BucketName = bucketName,
				ContentBody = body,
				ContentType = "application/json",
				Key = filePath,
			};

			return putRequest;
		}

		protected PutObjectRequest GeneratePutRequest<T>(T putMe)
			where T : DefinedByName, Unique
		{
			var filePath = filePathBuilder.GetFilePath(putMe);
			var putRequest = GenerateBasePutRequest(putMe, filePath);

			return putRequest;
		}

		protected PutObjectRequest GeneratePutRequest<T>(T putMe, Tick tick)
			where T : DefinedByName, Unique
		{
			var filePath = filePathBuilder.GetFilePath(putMe, tick);
			var putRequest = GenerateBasePutRequest(putMe, filePath);

			return putRequest;
		}

		protected async Task<T> GetAsync<T>(string id)
				where T : DefinedByName, Unique
		{
			var getRequest = GenerateBaseGetRequest(id);

			try
			{
				IAmazonS3 client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
				using (GetObjectResponse response = await client.GetObjectAsync(getRequest))
					return JsonSerializer.Deserialize<T>(response.ResponseStream)
						?? throw new FileNotFoundException();
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
					throw new Exception($"Error occurred:  {amazonS3Exception.Message}:::{getRequest.BucketName}=>{getRequest.Key}");
				}
			}
		}

		protected async Task<T> PutAsync<T>(LogItem<T> putMe)
			where T : class
		{
			if (putMe.Payload == null)
				throw new NullReferenceException();

			var putRequest = GenerateLogPutRequest(putMe);

			try
			{
				IAmazonS3 client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
				PutObjectResponse response = await client.PutObjectAsync(putRequest);
				return putMe.Payload;
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
					throw new Exception($"Error occurred:  {amazonS3Exception.Message}:::{putRequest.BucketName}=>{putRequest.Key}");
				}
			}
		}

		protected async Task<T> PutAsync<T>(T putMe, Tick tick)
			where T : DefinedByName, Unique
		{
			var putRequest = GeneratePutRequest(putMe, tick);

			try
			{
				IAmazonS3 client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
				PutObjectResponse response = await client.PutObjectAsync(putRequest);
				return putMe;
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
					throw new Exception($"Error occurred:  {amazonS3Exception.Message}:::{putRequest.BucketName}=>{putRequest.Key}");
				}
			}
		}

		private PutObjectRequest GenerateLogPutRequest<T>(T putMe)
					where T : LogItem
		{
			var filePath = $"logs/{typeof(T).GenericTypeArguments[0].Name.ToLower()}/{putMe.Id}.json";
			var body = JsonSerializer.Serialize(putMe);
			var putRequest = new PutObjectRequest()
			{
				BucketName = bucketName,
				ContentBody = body,
				ContentType = "application/json",
				Key = filePath,
			};
			return putRequest;
		}

		private T SetIdIfMissing<T>(T obj, string id)
			where T : DefinedByName, Unique
		{
			obj.Id = string.IsNullOrEmpty(obj.Id) ? id : obj.Id;
			return obj;
		}
	}
}