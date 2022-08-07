using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using System.Text;

namespace IWS.Water.Get.Api.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestToUpperFunction()
    {
        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        using (Stream stream = new MemoryStream())
        {
            var id = "Water/operations/standard-tick-.json";
            stream.Write(Encoding.UTF8.GetBytes(id));
            stream.Position = 0;

            var responseStream = await function.FunctionHandler(stream);

            Assert.False(true);
        }
    }
}
