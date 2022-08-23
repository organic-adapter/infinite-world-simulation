namespace AEW.ShowCases.Access
{
	public class HiveEndpointAccess : IHiveEndpointAccess
	{
		private const string BASE_END_POINT = "https://fpnfkk4jpg.execute-api.us-east-1.amazonaws.com/public";

		public async IAsyncEnumerator<string> GetEndpointsAsync()
		{
			yield return $"{BASE_END_POINT}/water";
			yield return $"{BASE_END_POINT}/food";
		}
	}
}