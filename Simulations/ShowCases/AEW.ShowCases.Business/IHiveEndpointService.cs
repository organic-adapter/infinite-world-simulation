using AEW.Contracts.ShowCases;

namespace AEW.ShowCases.Business
{
	public interface IHiveEndpointService
	{
		public Task<IEnumerable<HiveDetails>> GetEndpointsAsync();
	}
}