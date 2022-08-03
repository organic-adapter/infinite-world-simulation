using Amazon.Lambda.TestUtilities;
using MessageQueues.Messages;
using Population.Business.Food;
using System.Text.Json;
using Xunit;

namespace FoodDemandHandler.Tests;

public class FunctionTest
{
	[Fact]
	public void TestToUpperFunction()
	{
		// Invoke the lambda function and confirm the string was upper cased.
		var function = new FoodDemandSubscriberAWS();
		var context = new TestLambdaContext();
		const string supplyName = "Sent from Unit Test";
		const float supplyUnits = 987.65f;
		var messageBody = new MessageBody<FoodSupply>() { Payload = new FoodSupply() { Name = supplyName, Units = supplyUnits } };
		var message = new AwsSqsMessage() { Records = new() { new AwsSqsRecord() { Body = JsonSerializer.Serialize(messageBody) } } };
		var json = JsonSerializer.Serialize(message);
		var response = function.FunctionHandler(message, context);

		Assert.True(response.Successful);
	}
}