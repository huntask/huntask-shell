using Microsoft.AspNetCore.Identity;
using Huntask.Identity.Service.Infrastructure.Contexts;
using Huntask.Identity.Service.Infrastructure.Models.Options;
using CorrelationId.DependencyInjection;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
  public static void RegisterAspNetServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services
      .AddAuth(builder)
      .AddCorrelationId()
      .AddSwagger()
      .ConfigureCors(builder)
      .ConfigureCache();
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
    services.AddOpenApiDocument(config =>
    {
      config.DocumentName = "Huntask.Identity.Api";
      config.Title = "Huntask.Identity.Api";
      config.Version = "v1";
    });

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
          builder => {
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
}