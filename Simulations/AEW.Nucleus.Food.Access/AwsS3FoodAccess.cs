using AEW.Common.Access.Aws.S3;
using AEW.Contracts;
using AEW.Events;
using AEW.Nucleus.Food.Access.Models;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace AEW.Nucleus.Food.Access
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

		[Obsolete("These are going to save as log items. Instead of saving them to the S3 we will need a different resource in the future.")]
		public async Task SaveAsync(SupplyDemanded demand)
		{
			var logItem = new LogItem<SupplyDemanded>()
			{
				Id = Guid.NewGuid().ToString(),
				Payload = demand,
				Timestamp = DateTime.Now.ToString("o", CultureInfo.InvariantCulture),
			};
			await PutAsync(logItem);
		}

		[Obsolete("These are going to save as log items. Instead of saving them to the S3 we will need a different resource in the future.")]
		public async Task SaveAsync(SupplyDispatched dispatch)
		{
			var logItem = new LogItem<SupplyDispatched>()
			{
				Id = Guid.NewGuid().ToString(),
				Payload = dispatch,
				Timestamp = DateTime.Now.ToString("o", CultureInfo.InvariantCulture),
			};
			await PutAsync(logItem);
		}
	}
}