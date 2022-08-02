using Xunit;
using Amazon.Lambda.TestUtilities;
using Food.Supply.POC.Contracts;

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
        var message = new FoodSupplied() { Payload = new Supply() { Name = supplyName } };
        var response = function.FunctionHandler(message, context);

        Assert.Equal(supplyName, response.Name);
    }
}
