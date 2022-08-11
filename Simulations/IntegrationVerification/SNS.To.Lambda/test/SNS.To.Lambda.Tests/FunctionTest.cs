using AEW.Events.Nucleus.Water;
using Amazon.Lambda.SNSEvents;
using Amazon.Lambda.TestUtilities;
using System.Text.Json;
using Xunit;
using static Amazon.Lambda.SNSEvents.SNSEvent;

namespace SNS.To.Lambda.Tests;

public class FunctionTest
{
	[Fact]
	public async Task TestWaterDemanded()
	{
		var function = new Function();
		var context = new TestLambdaContext();
		var waterDemandedJson = JsonSerializer.Serialize(new WaterDemanded() { Payload = new AEW.Contracts.Demand() { Quantity = 2.0f, SupplyType = "Water" } });
		var messageAttributes = new Dictionary<string, MessageAttribute>() { { "bucketName", new MessageAttribute() { Value = "iws-water-proto-001", Type = "String" } } };
		var snsEvent = new SNSEvent()
		{
			Records = new List<SNSRecord>(){
				new SNSRecord()
				{
					EventSource = "Unit Test",
					Sns = new SNSMessage()
					{
						Message = waterDemandedJson,
						MessageAttributes = messageAttributes,
					}
				}
			}
		};
		var json = JsonSerializer.Serialize(snsEvent);
		await function.FunctionHandler(snsEvent, context);
	}
}