using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using IWS.Contracts.Shelter;
using IWS.Shelter.Business;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<IWS.Shelter.Api.HttpApiJsonSerializerContext>))]

namespace IWS.Shelter.Api;

[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
public partial class HttpApiJsonSerializerContext : JsonSerializerContext
{
}

public class Function
{
	private readonly Dictionary<HttpMethod, Func<APIGatewayProxyRequest, Task<APIGatewayProxyResponse>>> methodMap;
	private readonly ShelterManager shelterManager;

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

		shelterManager = provider.GetService<ShelterManager>()
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
		var shelterTick = await shelterManager.GetAsync(id);
		var body = JsonSerializer.Serialize(shelterTick);

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = body,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}

	public async Task<APIGatewayProxyResponse> HandleSave(APIGatewayProxyRequest input)
	{
		var saveMe = JsonSerializer.Deserialize<ShelterTick>(input.Body);
		var saved = await shelterManager.SaveAsync(saveMe);
		var body = JsonSerializer.Serialize(saved);

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = body,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}
}