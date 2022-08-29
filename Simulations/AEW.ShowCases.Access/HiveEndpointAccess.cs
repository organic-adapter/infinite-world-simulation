using AEW.Contracts.ShowCases;
using Microsoft.Extensions.Options;

namespace AEW.ShowCases.Access
{
	public class HiveEndpointAccess : IHiveEndpointAccess
	{
		private readonly IOptionsMonitor<EndpointConfiguration> options;

		public HiveEndpointAccess(IOptionsMonitor<EndpointConfiguration> options)
		{
			this.options = options;
		}

		private string BaseEndPoint => options.CurrentValue.BaseEndPoint;
		private string Name => options.CurrentValue.HiveName;
		public async Task<IEnumerable<HiveDetails>> GetEndpointsAsync()
		{
			return await Task.Run(
				() => new List<HiveDetails>()
				{
					new() { Name = Name, BaseEndpoint = $"{BaseEndPoint}" },
				}
			);
		}
	}
}