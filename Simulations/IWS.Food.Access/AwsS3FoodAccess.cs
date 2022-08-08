using IWS.Common.Access.Aws.S3;
using IWS.Food.Access.Models;
using Microsoft.Extensions.Options;

namespace IWS.Food.Access
{
	public class AwsS3FoodAccess : BaseAccess, FoodAccess
	{
		private readonly IOptionsMonitor<AccessConfiguration<AwsS3FoodAccess>> options;

		public AwsS3FoodAccess
				(
					IOptionsMonitor<AccessConfiguration<AwsS3FoodAccess>> options
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

		public async Task<FoodTick> RetrieveAsync(string id)
		{
			return await GetAsync<FoodTick>(id);
		}

		public async Task<FoodTick> SaveAsync(FoodTick FoodTick)
		{
			return await PutAsync(FoodTick, FoodTick.Tick);
		}
	}
}