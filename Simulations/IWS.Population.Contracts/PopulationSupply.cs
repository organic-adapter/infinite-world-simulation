namespace IWS.Contracts.Population
{
	public class PopulationSupply : Supply
	{
		public PopulationSupply()
		{
			Type = GetType().FullName ?? Type;
		}
	}
}