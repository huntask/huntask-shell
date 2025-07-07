using Microsoft.AspNetCore.Identity;
using Huntask.Identity.Service.Infrastructure.Contexts;
using Huntask.Identity.Service.Infrastructure.Models.Options;
using CorrelationId.DependencyInjection;
using Huntask.Identity.Service.Application.Commands.RegisterUserCommand;
using MediatR;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Huntask.Identity.Service.Presentation.Swagger;
using Microsoft.OpenApi.Models;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
  public static void RegisterAspNetServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services
      .AddWebApiControllers()
      .AddApiVersioning()
      .AddCorrelationId()
      .AddSwagger()
      .ConfigureCache()
      .AddMediatR()
      .AddAuth(builder)
      .ConfigureCors(builder);
  }

  private static IServiceCollection AddWebApiControllers(this IServiceCollection services)
  {
    services
      .AddControllers()
      .AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
      });

    return services;
  }

  private static IServiceCollection AddApiVersioning(this IServiceCollection services)
  {
    services.AddApiVersioning(options =>
    {
      options.ReportApiVersions = true;
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.DefaultApiVersion = new ApiVersion(1, 0);
    });

    services.AddVersionedApiExplorer(options =>
    {
      options.GroupNameFormat = "'v'VVV";
      options.SubstituteApiVersionInUrl = true;
    });

    return services;
  }

  private static IServiceCollection AddAuth(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
    services.AddIdentity<IdentityUser, IdentityRole>()
      .AddEntityFrameworkStores<IdentityContext>()
      .AddDefaultTokenProviders();

    return services;
  }

  private static IServiceCollection AddCorrelationId(this IServiceCollection services)
  {
    services.AddDefaultCorrelationId(options =>
    {
      options.IncludeInResponse = true;
      options.RequestHeader = "X-Correlation-ID";
      options.UpdateTraceIdentifier = true;
    });

    return services;
  }

  private static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.ConfigureOptions<ConfigureSwaggerOptions>();

    return services;
  }

  private static IServiceCollection ConfigureCors(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services.AddCors(options =>
    {
      if (builder.Environment.IsDevelopment())
      {
        options.AddPolicy(
          "AllowDevelopment",
          builder =>
          {
            builder
              .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
              .AllowAnyHeader()
              .AllowAnyMethod();
          }
        );
      }
    });

    return services;
  }

  private static IServiceCollection ConfigureCache(this IServiceCollection services)
  {
    services.AddOutputCache();
    services.AddResponseCaching();

    return services;
  }

  private static IServiceCollection AddMediatR(this IServiceCollection services)
  {
    services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
    return services;
  }
}