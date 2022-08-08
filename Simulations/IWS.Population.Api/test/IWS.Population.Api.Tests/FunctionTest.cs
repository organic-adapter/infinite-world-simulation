using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace IWS.Population.Api.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestGetRequest()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new APIGatewayProxyRequest()
        {
            HttpMethod = HttpMethod.Get.Method
            ,
            PathParameters = new Dictionary<string, string>() { { "id", "standard-tick.json" } }
        };
        var upperCase = await function.FunctionHandler(request, context);

        //Assert.Equal("HELLO WORLD", upperCase);
    }
}
