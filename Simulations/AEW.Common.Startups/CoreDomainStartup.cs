namespace AEW.Common.Startups
{
	public static class CoreDomainStartup
	{
		public static Action<Common.Access.Aws.S3.AccessConfiguration<T>> BuildAccessConfiguration<T>()
			where T : class
		{
			return (Common.Access.Aws.S3.AccessConfiguration<T> options) =>
			{
				options.BucketName = Environment.GetEnvironmentVariable("HIVE__BUCKET__NAME") ?? string.Empty;
				options.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("HIVE__BUCKET__REGION"));
			};
		}
	}
}