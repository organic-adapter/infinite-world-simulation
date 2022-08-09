using Amazon.Lambda.TestUtilities;
using IWS.Events.Water;
using MessageQueues.Messages;
using System.Text.Json;
using Xunit;

namespace IWS.Water.Subscriber.Tests;

public class FunctionTest
{
	[Fact]
	public async Task TestWaterDemanded()
	{
		var function = new Function();
		var context = new TestLambdaContext();
		var waterDemandedJson = JsonSerializer.Serialize(new WaterDemanded() { Payload = new Contracts.Demand() { Quantity = 2.0f, SupplyType = "Water" } });
		var request = new AwsSnsMessage()
		{
			Records = new List<AwsSnsRecord>
			{
				new AwsSnsRecord()
				{
					EventSource = "Unit Test",
					Sns = new AwsSns()
					{
						Message = waterDemandedJson,
						MessageId = Guid.NewGuid().ToString(),
						Timestamp = DateTime.Now.ToString(),
						Signature = "Unit Test Signature",
						SignatureVersion = "Unit Test Signature Version"
					}
				}
			}
		};
		using (var stream = new MemoryStream())
		{
			var writer = new StreamWriter(stream);
			try
			{
				var json = JsonSerializer.Serialize(request);
				writer.Write(json);
				writer.Flush();
				stream.Position = 0;

				await function.FunctionHandler(stream);
			}
			finally
			{
				writer.Dispose();
			}
		}
	}
}