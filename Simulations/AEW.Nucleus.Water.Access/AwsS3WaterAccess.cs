using AEW.Common.Access.Aws.S3;
using AEW.Contracts;
using AEW.Events;
using AEW.Nucleus.Water.Access.Models;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace AEW.Nucleus.Water.Access
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