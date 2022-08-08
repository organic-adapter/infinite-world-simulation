using AutoMapper;
using IWS.Contracts.Shelter;
using IWS.Shelter.Access;

namespace IWS.Shelter.Business
{
	public class AnatomyShelterManager : ShelterManager
	{
		private readonly IMapper mapper;
		private readonly ShelterAccess shelterAccess;

		public AnatomyShelterManager
				(
					IMapper mapper,
					ShelterAccess ShelterAccess
				)
		{
			this.mapper = mapper;
			this.shelterAccess = ShelterAccess;
		}

		public async Task<ShelterTick> GetAsync(string id)
		{
			return await shelterAccess.RetrieveAsync(id);
		}

		public async Task<ShelterTick> SaveAsync(ShelterTick? ShelterTick)
		{
			var saveMe = mapper.Map<Access.Models.ShelterTick>(ShelterTick);
			//TODO: tick-history needs to be saved here as well.
			return await shelterAccess.SaveAsync(saveMe);
		}
	}
}