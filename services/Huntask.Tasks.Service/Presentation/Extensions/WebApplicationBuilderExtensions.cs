using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
  private const string LogTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [CorrelationId] {Message:lj}{NewLine}{Exception}";

  public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
  {
    builder
      .ConfigureLogs()
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

  private static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
  {
    if (builder.Environment.IsDevelopment())
    {
      builder.WebHost.ConfigureKestrel(options =>
      {
        options.ListenAnyIP(builder.Configuration.GetValue<int>("HuntaskPorts__TasksServiceHttp", 8080));
      });
    }

    return builder;
  }
}