using Amazon;

namespace AEW.Common.Access.Aws.Sns
{
	public class BusConfiguration<T>
		where T : NotificationBus
	{
		public BusConfiguration()
		{
			RegionEndpoint = RegionEndpoint.USEast1;
			TopicArn = string.Empty;
		}

		public RegionEndpoint RegionEndpoint { get; set; }
		public string TopicArn { get; set; }
	}
}