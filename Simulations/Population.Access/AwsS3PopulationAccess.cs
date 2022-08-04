using IWS.Common.Access.Aws.S3;
using Microsoft.Extensions.Options;
using Population.Access.Models;

namespace IWS.Population.Access
{
	public class AwsS3PopulationAccess : BaseAccess, PopulationAccess
	{
		private readonly IOptionsMonitor<AccessConfiguration<AwsS3PopulationAccess>> options;

		public AwsS3PopulationAccess
				(
					IOptionsMonitor<AccessConfiguration<AwsS3PopulationAccess>> options
					, FilePathBuilder filePathBuilder
				)
				: base(options.CurrentValue.BucketName, filePathBuilder)
		{
			this.options = options;
		}

		public async Task SaveAsync(PopulationTick populationTick)
		{
			await PutAsync(populationTick, populationTick.Tick);
		}
	}
}