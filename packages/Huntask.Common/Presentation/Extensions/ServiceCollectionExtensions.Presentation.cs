using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Huntask.Common.Presentation.Extensions;

public static class ServiceCollectionPresentationExtensions
{
  public static IServiceCollection AddCommonPresentationServices(this IServiceCollection services)
  {
    services
      .AddApiControllers()
      .AddApiVersioning()
      .AddAuth()
      .AddCors();

    return services;
  }

  private static IServiceCollection AddApiControllers(this IServiceCollection services)
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

  private static IServiceCollection AddAuth(this IServiceCollection services)
  {
    var jwtOptions = services
      .BuildServiceProvider()
      .GetRequiredService<IOptionsSnapshot<JwtOptions>>()
      .Value;

    services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = jwtOptions.GetTokenValidationParameters();
        options.Events = GetJwtBearerEvents();
      });
    services.AddAuthorization();

    return services;
  }

  private static JwtBearerEvents GetJwtBearerEvents()
  {
    return new JwtBearerEvents
    {
      OnMessageReceived = context =>
      {
        if (context.Request.Cookies.TryGetValue(Constants.Cookies.AuthToken, out var token))
        {
          context.Token = token;
        }

        return Task.CompletedTask;
      }
    };
  }

  private static IServiceCollection AddCors(this IServiceCollection services)
  {
    services.AddCors(options =>
    {
      var corsOptions = services
        .BuildServiceProvider()
        .GetRequiredService<IOptionsSnapshot<CorsOptions>>()
        .Value;

      options.AddPolicy(
        "AllowOrigins",
        builder =>
        {
          builder
            .WithOrigins(corsOptions.AllowOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        }
      );
    });

    return services;
  }
}