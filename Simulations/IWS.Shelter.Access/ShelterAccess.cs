using IWS.Shelter.Access.Models;

namespace IWS.Shelter.Access
{
	public interface ShelterAccess
	{
		public Task RemoveAsync(string id);

		public Task<ShelterTick> RetrieveAsync(string id);

		public Task<ShelterTick> SaveAsync(ShelterTick waterTick);
	}
}