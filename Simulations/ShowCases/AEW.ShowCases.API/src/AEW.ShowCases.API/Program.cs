using System.Text.Json;
using Amazon;
using AEW.ShowCases.Access;
using AEW.ShowCases.Business;

var builder = WebApplication.CreateBuilder(args);

var localOnlyOrigins = "_localOnlyOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: localOnlyOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8080");
                      });
});

//Logger
builder.Logging
        .ClearProviders()
        .AddJsonConsole();
 
// Add services to the container.
builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

string region = Environment.GetEnvironmentVariable("AWS_REGION") ?? RegionEndpoint.USEast1.SystemName;
string baseEndPoint = Environment.GetEnvironmentVariable("BASE_END_POINT") ?? string.Empty;
string hiveName = Environment.GetEnvironmentVariable("HIVE_NAME") ?? string.Empty;

builder.Services
        .Configure<EndpointConfiguration>((options) => 
        {
            options.BaseEndPoint = baseEndPoint;
            options.HiveName = hiveName;
        })
        .AddTransient<IHiveEndpointAccess, HiveEndpointAccess>()
        .AddTransient<IHiveEndpointService, HiveEndpointService>();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);


var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(localOnlyOrigins);

app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
