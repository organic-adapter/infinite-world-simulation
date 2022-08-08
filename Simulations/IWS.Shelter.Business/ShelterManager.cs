using IWS.Contracts.Shelter;

namespace IWS.Shelter.Business
{
	public interface ShelterManager
	{
		public Task<ShelterTick> GetAsync(string id);

		public Task<ShelterTick> SaveAsync(ShelterTick? waterTick);
	}
}