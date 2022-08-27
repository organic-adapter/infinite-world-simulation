namespace AEW.ShowCases.Access
{
	public class EndpointConfiguration
	{
		[Obsolete("This can be retired when we get the means to persistently store each hive end point as they are spun up.")]
		public string BaseEndPoint { get; set; } = string.Empty;

		[Obsolete("This can be retired when we get the means to persistently store each hive end point as they are spun up.")]
		public string HiveName { get; set; } = string.Empty;
	}
}