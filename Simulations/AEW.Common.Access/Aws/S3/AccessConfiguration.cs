namespace AEW.Common.Access.Aws.S3
{
	public class AccessConfiguration<T>
		where T : class
	{
		public AccessConfiguration()
		{
			RegionEndpoint = Amazon.RegionEndpoint.USEast1;
		}

		public string BucketName { get; set; } = string.Empty;
		public Amazon.RegionEndpoint RegionEndpoint { get; set; }
	}
}