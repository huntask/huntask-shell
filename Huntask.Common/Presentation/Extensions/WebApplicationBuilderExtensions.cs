using Microsoft.AspNetCore.Builder;
using Serilog.Sinks.SystemConsole.Themes;

namespace Huntask.Common.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder ConfigureCommonBuilder(this WebApplicationBuilder builder)
  {
    builder
      .ConfigureLogs();

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
}