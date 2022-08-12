using AEW.Common.Startups;
using AEW.Nucleus.Food.Access;
using AEW.Nucleus.Food.Business;
using AEW.Nucleus.Food.Business.ModelMaps;
using Amazon;
using System.Text.Json;
using static AEW.Common.DomainHierarchy;

var builder = WebApplication.CreateBuilder(args);

//Logger
builder.Logging
		.ClearProviders()
		.AddJsonConsole();

string region = Environment.GetEnvironmentVariable("AWS_REGION") ?? RegionEndpoint.USEast2.SystemName;
var domainName = AEW.Contracts.Nucleus.Food.Constants.DomainName;
var coreSupplyBusConfiguration = AwsSnsStartup.GetCoreSupplyBusConfig();
builder.Services
	.AddAutoMapperWith(typeof(FoodMappingProfiles))
	.Configure(CoreDomainStartup.BuildAccessConfiguration<AwsS3FoodAccess>())
	.Configure(AwsSnsStartup.BuildOptions(coreSupplyBusConfiguration))
	.AddSnsClient(coreSupplyBusConfiguration)
	.AddAwsSnsCoreSupplyBus()
	.AddAccessDefaults(domainName)
	.AddSingleton(new HierarchyTree(domainName));

builder.Services
	.AddTransient<FoodAccess, AwsS3FoodAccess>()
	.AddTransient<FoodManager, AnatomyFoodManager>();

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