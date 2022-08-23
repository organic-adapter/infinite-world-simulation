namespace AEW.ShowCases.Access
{
	public interface IHiveEndpointAccess
	{
		public IAsyncEnumerator<string> GetEndpointsAsync();
	}
}