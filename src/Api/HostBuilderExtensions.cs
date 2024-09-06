#region

using System.IO.Compression;
using System.Reflection;
using Api.Abstractions;
using Api.Handlers;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Infrastructure.Logging;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

#endregion

namespace Api;

public static class HostBuilderExtensions
{
    public static void AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        AddVersioning(builder);
        AddExceptionHandler(builder);
        AddDefaultHealthChecks(builder);
        AddDefaultOpenApi(builder);
        EnableCompression(builder);
        AddRefitLogging(builder);
    }
    
    public static void DisableHttp3(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ConfigureEndpointDefaults(listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });
    }
    
    public static void EnableCompression(this IHostApplicationBuilder builder)
    {
        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
        });

        builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });

        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });
    }

    public static void AddVersioning(this IHostApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            options.ApiVersionReader = ApiVersionReader.Combine(
                new MediaTypeApiVersionReader(),
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }
    
    private static void AddExceptionHandler(IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }
    
    public static void AddRefitLogging(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<HttpLoggingHandler>();
    }

    private static void AddDefaultOpenApi(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Title = $"{builder.Environment.ApplicationName} v{description.ApiVersion}",
                        Version = description.ApiVersion.ToString()
                    });
            }

            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
            options.CustomSchemaIds(type => type.ToString());
        });
    }

    private static void AddDefaultHealthChecks(IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
    }

    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly)
    {
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder =
            routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
    
    public static void MapDefaultEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
    
    public static void UseDefaultOpenApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }

    public static void UseCompression(this WebApplication app)
    {
        app.UseResponseCompression();
    }

    public static RouteGroupBuilder GetVersionGroup(this WebApplication app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        return app.MapGroup("api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
    }
}
