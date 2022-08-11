namespace AEW.Nucleus.Food.Business
{
	public interface FoodGeneratingEngine
	{
		public Task<Dictionary<string, float>> SuppliesNeededAsync();
	}
}