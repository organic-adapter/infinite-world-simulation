using IWS.Common.Access.Aws.S3;
using IWS.Shelter.Access.Models;
using Microsoft.Extensions.Options;

namespace IWS.Shelter.Access
{
	public class AwsS3ShelterAccess : BaseAccess, ShelterAccess
	{
		private readonly IOptionsMonitor<AccessConfiguration<AwsS3ShelterAccess>> options;

		public AwsS3ShelterAccess
				(
					IOptionsMonitor<AccessConfiguration<AwsS3ShelterAccess>> options
					, FilePathBuilder filePathBuilder
				)
				: base(options.CurrentValue.BucketName, filePathBuilder)
		{
			this.options = options;
		}

		public async Task RemoveAsync(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<ShelterTick> RetrieveAsync(string id)
		{
			return await GetAsync<ShelterTick>(id);
		}

		public async Task<ShelterTick> SaveAsync(ShelterTick ShelterTick)
		{
			return await PutAsync(ShelterTick, ShelterTick.Tick);
		}
	}
}