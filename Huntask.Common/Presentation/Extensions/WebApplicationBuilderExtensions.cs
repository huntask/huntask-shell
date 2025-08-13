using System.Text;
using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Sinks.SystemConsole.Themes;

namespace Huntask.Common.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder ConfigureCommonBuilder(this WebApplicationBuilder builder)
  {
    builder
      .ConfigureLogs()
      .ConfigureCors()
      .ConfigureAuth();

    return builder;
  }

  private static WebApplicationBuilder ConfigureLogs(this WebApplicationBuilder builder)
  {
    Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.Console(theme: AnsiConsoleTheme.Code)
      .CreateBootstrapLogger();

    builder.Host.UseSerilog((context, services, loggerConfiguration) =>
    {
      loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
    });

    return builder;
  }

  private static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
  {
    builder.Services.AddCors(options =>
    {
      var allowedOrigins = builder.Configuration?.GetSection("Cors")?.GetValue<string[]>("AllowOrigins") ?? [];

      options.AddPolicy(
        "AllowOrigins",
        builder =>
        {
          builder
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        }
      );
    });

    return builder;
  }

  private static IServiceCollection ConfigureAuth(this WebApplicationBuilder builder)
  {
    var jwtOptionsConfig = builder.Configuration.GetSection("Jwt");
    ArgumentNullException.ThrowIfNull(jwtOptionsConfig, "Jwt configuration section is missing.");

    var services = builder.Services;
    services.Configure<JwtOptions>(jwtOptionsConfig);

    var jwtOptions = jwtOptionsConfig?.Get<JwtOptions>() ?? new JwtOptions();
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
}