namespace IWS.Common.Startups
{
	public static class CoreDomainStartup
	{
		public static Action<Common.Access.Aws.S3.AccessConfiguration<T>> BuildAccessConfiguration<T>()
			where T : class
		{
			return (Common.Access.Aws.S3.AccessConfiguration<T> options) =>
			{
				options.BucketName = Environment.GetEnvironmentVariable("DOMAIN__BUCKET__NAME") ?? string.Empty;
				options.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("DOMAIN__BUCKET__REGION"));
			};
		}
	}
}