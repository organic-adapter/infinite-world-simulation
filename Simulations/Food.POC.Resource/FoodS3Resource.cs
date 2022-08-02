using Amazon.S3;
using Amazon.S3.Model;

namespace Food.POC.Resource
{
	[Obsolete("Replace with a real domain and resource")]
	public class FoodS3Resource
	{
		private readonly string bucketName = "infinite-world-simulation-food";
		private readonly string fileNamePattern = "food-supply-{0}-.json";

		public async Task SaveAsync(string supply)
		{
			IAmazonS3 client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
			var dateStamp = DateTime.Now.ToString("yyyy-MM-ddThh");
			var key = string.Format(fileNamePattern, dateStamp);
			try
			{
				PutObjectRequest putRequest = new PutObjectRequest
				{
					BucketName = bucketName,
					Key = key,
					ContentBody = supply,
					ContentType = "application/json"
				};

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