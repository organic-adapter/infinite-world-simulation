using AEW.Contracts.ShowCases;
using AEW.ShowCases.Access;

namespace AEW.ShowCases.Business
{
	public class HiveEndpointService : IHiveEndpointService
	{
		private readonly IHiveEndpointAccess access;

		public HiveEndpointService(IHiveEndpointAccess access)
		{
			this.access = access;
		}

		public async Task<IEnumerable<HiveDetails>> GetEndpointsAsync()
		{
			return await access.GetEndpointsAsync();
		}
	}
}