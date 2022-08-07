using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using IWS.Contracts.Water;
using System.Text.Json;
using System.Text;

namespace IWS.Water.Save.Api.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var model = new WaterTick() { DomainName = Contracts.Water.Constants.DomainName };
        using (Stream stream = new MemoryStream())
        {
            var json = JsonSerializer.Serialize(model);
            stream.Write(Encoding.UTF8.GetBytes(json));
            stream.Position = 0;

            var responseStream = function.FunctionHandler(stream);

            Assert.False(true);
        }
    }
}
