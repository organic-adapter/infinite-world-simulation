using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using IWS.Contracts.Water;
using IWS.Water.Business;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<IWS.Water.Api.HttpApiJsonSerializerContext>))]

namespace IWS.Water.Api;

[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
public partial class HttpApiJsonSerializerContext : JsonSerializerContext
{
}

public class Function
{
	private readonly Dictionary<HttpMethod, Func<APIGatewayProxyRequest, Task<APIGatewayProxyResponse>>> methodMap;
	private readonly WaterManager waterManager;

	public Function()
	{
		methodMap = new();
		methodMap.Add(HttpMethod.Get, HandleGet);
		methodMap.Add(HttpMethod.Post, HandleSave);
		methodMap.Add(HttpMethod.Put, HandleSave);

		var provider = Startup.SetUp();
		waterManager = provider.GetService<WaterManager>();
	}

	public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
	{
		var method = new HttpMethod(input.HttpMethod) ?? HttpMethod.Head;
		return await methodMap[method](input);
	}

	public async Task<APIGatewayProxyResponse> HandleGet(APIGatewayProxyRequest input)
	{
		var id = HttpUtility.UrlDecode(input.PathParameters["id"]);
		var waterTick = await waterManager.GetAsync(id);
		var body = JsonSerializer.Serialize(waterTick);

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = body,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}

	public async Task<APIGatewayProxyResponse> HandleSave(APIGatewayProxyRequest input)
	{
		var saveMe = JsonSerializer.Deserialize<WaterTick>(input.Body);
		var saved = await waterManager.SaveAsync(saveMe);
		var body = JsonSerializer.Serialize(saved);

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = body,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}
}