using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Builder;
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
}