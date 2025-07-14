using Huntask.Assets.Service.Infrastructure.Contexts;
using Huntask.Assets.Service.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Identity;
using Serilog.Sinks.SystemConsole.Themes;

namespace Huntask.Assets.Service.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
  private const string LogTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [CorrelationId] {Message:lj}{NewLine}{Exception}";

  public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
  {
    builder
      .ConfigureOptions()
      .ConfigureLogs()
      .ConfigureCors()
      .ConfigureAuth()
      .ConfigureKestrel();

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

    return builder;
  }

  private static WebApplicationBuilder ConfigureAuth(this WebApplicationBuilder builder)
  {
    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

    return builder;
  }

  private static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
  {
    if (builder.Environment.IsDevelopment())
    {
      builder.WebHost.ConfigureKestrel(options =>
      {
        options.ListenAnyIP(builder.Configuration.GetValue<int>("HUNTASK_ASSETS_SERVICE_HTTP_PORT", 8082));
      });
    }

    return builder;
  }

  private static WebApplicationBuilder ConfigureOptions(this WebApplicationBuilder builder)
  {
    builder.Services.Configure<AssetsOptions>(builder.Configuration.GetSection("Assets"));

    return builder;
  }
}