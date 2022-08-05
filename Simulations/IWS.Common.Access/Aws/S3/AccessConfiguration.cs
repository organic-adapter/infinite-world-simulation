namespace IWS.Common.Access.Aws.S3
{
	public class AccessConfiguration<T>
		where T : class
	{
		public string BucketName { get; set; } = string.Empty;
	}
}