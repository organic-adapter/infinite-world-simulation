using System.Text.Json;
using Amazon;
using AEW.Common.Startups;
using AEW.Nucleus.Water.Access;
using AEW.Nucleus.Water.Business.ModelMaps;
using AEW.Common.Builders;
using static AEW.Common.DomainHierarchy;
using AEW.Nucleus.Water.Business;

var builder = WebApplication.CreateBuilder(args);

//Logger
builder.Logging
        .ClearProviders()
        .AddJsonConsole();

string region = Environment.GetEnvironmentVariable("AWS_REGION") ?? RegionEndpoint.USEast2.SystemName;
var domainName = AEW.Contracts.Nucleus.Water.Constants.DomainName;
var coreSupplyBusConfiguration = AwsSnsStartup.GetCoreSupplyBusConfig();
builder.Services
	.AddAutoMapperWith(typeof(WaterMappingProfiles))
	.Configure(CoreDomainStartup.BuildAccessConfiguration<AwsS3WaterAccess>())
	.Configure(AwsSnsStartup.BuildOptions(coreSupplyBusConfiguration))
	.AddSnsClient(coreSupplyBusConfiguration)
	.AddAwsSnsCoreSupplyBus()
	.AddAccessDefaults(domainName)
	.AddSingleton(new HierarchyTree(domainName));

builder.Services
	.AddTransient<WaterAccess, AwsS3WaterAccess>()
	.AddTransient<WaterManager, AnatomyWaterManager>();

#if DEBUG
	builder.Services.AddSingleton(builder.Services);
#endif



// Add services to the container.
builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
