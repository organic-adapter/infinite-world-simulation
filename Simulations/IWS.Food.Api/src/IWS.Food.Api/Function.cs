using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using IWS.Contracts.Food;
using IWS.Food.Business;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<IWS.Food.Api.HttpApiJsonSerializerContext>))]

namespace IWS.Food.Api;

[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
public partial class HttpApiJsonSerializerContext : JsonSerializerContext
{
}

public class Function
{
	private readonly Dictionary<HttpMethod, Func<APIGatewayProxyRequest, Task<APIGatewayProxyResponse>>> methodMap;
	private readonly FoodManager foodManager;

	public Function() : this(Startup.SetUp())
	{
	}

	/// <summary>
	/// We need a Unit Testable version of this. We will inject the dependencies using this constructor instead.
	/// </summary>
	/// <param name="provider"></param>
	public Function(IServiceProvider provider)
	{
		methodMap = new()
		{
			{ HttpMethod.Get, HandleGet },
			{ HttpMethod.Post, HandleSave },
			{ HttpMethod.Put, HandleSave }
		};

		foodManager = provider.GetService<FoodManager>()
						?? throw new Exception("Service not found");
	}

	public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
	{
		var method = new HttpMethod(input.HttpMethod) ?? HttpMethod.Head;
		return await methodMap[method](input);
	}

	public async Task<APIGatewayProxyResponse> HandleGet(APIGatewayProxyRequest input)
	{
		var id = HttpUtility.UrlDecode(input.PathParameters["id"]);
		var foodTick = await foodManager.GetAsync(id);
		var body = JsonSerializer.Serialize(foodTick);

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = body,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}

	public async Task<APIGatewayProxyResponse> HandleSave(APIGatewayProxyRequest input)
	{
		var saveMe = JsonSerializer.Deserialize<FoodTick>(input.Body);
		var saved = await foodManager.SaveAsync(saveMe);
		var body = JsonSerializer.Serialize(saved);

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = body,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}
}