using System.Text.Json;
using System.Text.Json.Serialization;
using Api;
using Api.Configurations;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UseCases;
LoggingConfiguration.ConfigureSerilog();

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.ConfigureInfrastructureLayer();
builder.ConfigureUseCasesLayer();
builder.AddServiceDefaults();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

RegisterHttpClients(builder);
AddAuthentication(builder);
AddAuthorization(builder);
AddHttpAccessor(builder);
ConfigureSwagger(builder);
ConfigureSerialization(builder);
builder.DisableHttp3();

var app = builder.Build();
var versionGroup = app.GetVersionGroup();

app.UseExceptionHandler(opt => { });
app.UseForwardedHeaders();
app.UseCompression();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints(versionGroup);
app.MapDefaultEndpoints();
app.AddRequestLogging();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDefaultOpenApi();
}

await app.RunAsync();
return;

void AddAuthentication(IHostApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.AddAuthentication();
}

void AddAuthorization(IHostApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddAuthorizationBuilder();
}

void RegisterHttpClients(IHostApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddHttpClient();
}


void AddHttpAccessor(IHostApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}

void ConfigureSwagger(IHostApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.ConfigureSwaggerGen(c =>
    {
        c.UseAllOfToExtendReferenceSchemas();
        c.UseAllOfForInheritance();
        c.UseOneOfForPolymorphism();

        string[] discriminatorTypes = [];

        c.SelectDiscriminatorNameUsing(type => discriminatorTypes.Contains(type.Name) ? "$type" : null);
    });
}


void ConfigureSerialization(IHostApplicationBuilder webApplicationBuilder)
{
    builder.Services.Configure<JsonSerializerSettings>(options =>
    {
        options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.Converters.Add(
            new StringEnumConverter
            {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
            });
    });
    builder.Services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.WriteIndented = true;
        options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });
}