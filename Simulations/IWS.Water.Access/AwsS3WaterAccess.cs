using IWS.Common.Access.Aws.S3;
using IWS.Water.Access.Models;
using Microsoft.Extensions.Options;

namespace IWS.Water.Access
{
	public class AwsS3WaterAccess : BaseAccess, WaterAccess
	{
		private readonly IOptionsMonitor<AccessConfiguration<AwsS3WaterAccess>> options;

		public AwsS3WaterAccess
				(
					IOptionsMonitor<AccessConfiguration<AwsS3WaterAccess>> options
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

		public async Task<WaterTick> RetrieveAsync(string id)
		{
			return await GetAsync<WaterTick>(id);
		}

		public async Task<WaterTick> SaveAsync(WaterTick WaterTick)
		{
			return await PutAsync(WaterTick, WaterTick.Tick);
		}
	}
}