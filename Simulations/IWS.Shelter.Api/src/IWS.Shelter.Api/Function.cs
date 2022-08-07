using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

[assembly: LambdaSerializer(typeof(SourceGeneratorLambdaJsonSerializer<IWS.Shelter.Api.HttpApiJsonSerializerContext>))]

namespace IWS.Shelter.Api;

[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
public partial class HttpApiJsonSerializerContext : JsonSerializerContext
{
}

public class Function
{
	private readonly Dictionary<HttpMethod, Func<APIGatewayProxyRequest, APIGatewayProxyResponse>> methodMap = new Dictionary<HttpMethod, Func<APIGatewayProxyRequest, APIGatewayProxyResponse>>();

	public Function()
	{
		methodMap.Add(HttpMethod.Get, HandleGet);
		methodMap.Add(HttpMethod.Post, HandleSave);
		methodMap.Add(HttpMethod.Put, HandleSave);
		methodMap.Add(HttpMethod.Delete, HandleDelete);
	}

	public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
	{
		var method = new HttpMethod(input.HttpMethod) ?? HttpMethod.Head;
		return methodMap[method](input);
	}

	public APIGatewayProxyResponse HandleDelete(APIGatewayProxyRequest input)
	{
		var json = JsonSerializer.Serialize(new { Message = "This was a DELETE" });

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = json,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}

	public APIGatewayProxyResponse HandleGet(APIGatewayProxyRequest input)
	{
		var json = JsonSerializer.Serialize(new { Message = "This was a GET" });

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = json,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}

	public APIGatewayProxyResponse HandleSave(APIGatewayProxyRequest input)
	{
		var json = JsonSerializer.Serialize(new { Message = $"This was a {input.HttpMethod}" });

		return new APIGatewayProxyResponse()
		{
			StatusCode = (int)HttpStatusCode.OK,
			Body = json,
			Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
		};
	}
}