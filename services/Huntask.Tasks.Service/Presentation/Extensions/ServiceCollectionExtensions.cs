using System.Text;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using Huntask.Identity.Service.Infrastructure.Models.Options;

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
    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>() ?? new JwtOptions();

    services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidIssuer = jwtOptions.Issuer,
          ValidateAudience = true,
          ValidAudience = jwtOptions.Audience,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };
      });
    services.AddAuthorization();

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
      config.DocumentName = "Huntask.Tasks.Api";
      config.Title = "Huntask.Tasks.Api";
      config.Version = "v1";

      config.AddSecurity("JWT", new OpenApiSecurityScheme
      {
        Type = OpenApiSecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Enter the token"
      });
      config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
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