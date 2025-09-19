using Huntask.Common.Infrastructure.Models.Options;
using MassTransit;
using MassTransit.MessageData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using static Huntask.Common.Constants;

namespace Huntask.Common.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddSettings(this IServiceCollection services)
  {
    services
      .AddValidatedOptions<JwtOptions>("Jwt")
      .AddValidatedOptions<CorsOptions>("Cors")
      .AddValidatedOptions<ConnectionStringsOptions>("ConnectionStrings")
      .AddValidatedOptions<RabbitMqOptions>("RabbitMQ")
      .AddValidatedOptions<MassTransitOptions>("MassTransit")
      .AddFileSizesOptions();

    return services;
  }

  public static IServiceCollection AddValidatedOptions<T>(this IServiceCollection services, string configurationSection)
    where T : class, new()
  {
    services
      .AddOptions<T>()
      .BindConfiguration(configurationSection)
      .ValidateDataAnnotations()
      .ValidateOnStart();

    return services;
  }

  public static IServiceCollection AddFileSizesOptions(this IServiceCollection services)
  {
    services
      .AddOptions<FileSizesOptions>()
      .BindConfiguration("FileSizes")
      .PostConfigure(options =>
      {
        options.MaxAssetFileSize *= SizeUnitsConstants.MB;
        options.MessageDataThresholdSize *= SizeUnitsConstants.MB;
        options.MaxAvatarFileSize *= SizeUnitsConstants.MB;
      })
      .ValidateDataAnnotations()
      .ValidateOnStart();

    return services;
  }

  public static IServiceCollection AddMessageBroker<T>(this IServiceCollection services, Action<IBusRegistrationConfigurator>? registerConsumers = null)
    where T : DbContext
  {
    services.AddSingleton<IMessageDataRepository>(sp =>
    {
      var massTransitOptions = sp.GetRequiredService<IOptions<MassTransitOptions>>().Value;
      var fileSizesOptions = sp.GetRequiredService<IOptions<FileSizesOptions>>().Value;

      var dataDirPath = massTransitOptions.TempDataDirectory;
      var dataDir = new DirectoryInfo(dataDirPath);
      if (!dataDir.Exists)
      {
        dataDir.Create();
      }

      MessageDataDefaults.TimeToLive = TimeSpan.FromDays(1);
      MessageDataDefaults.ExtraTimeToLive = TimeSpan.FromMinutes(5);
      MessageDataDefaults.Threshold = fileSizesOptions.MessageDataThresholdSize;
      MessageDataDefaults.AlwaysWriteToRepository = false;

      return new FileSystemMessageDataRepository(dataDir);
    });

    services.AddMassTransit(bus =>
    {
      bus.SetKebabCaseEndpointNameFormatter();

      bus.AddEntityFrameworkOutbox<T>(o =>
      {
        o.UsePostgres();
        o.UseBusOutbox();
      });

      bus.AddConfigureEndpointsCallback((context, name, ep) =>
      {
        ep.UseEntityFrameworkOutbox<T>(context);
        ep.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(5)));
      });

      bus.ConfigureHealthCheckOptions(options =>
        {
          options.Name = "masstransit";
          options.MinimalFailureStatus = HealthStatus.Unhealthy;
          options.Tags.Add("ms");
        });

      if (registerConsumers is not null)
      {
        registerConsumers(bus);
      }

      bus.UsingRabbitMq((context, cfg) =>
      {
        var connectionString = context.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value.RabbitMqConnection;
        var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
        var messageDataRepository = context.GetRequiredService<IMessageDataRepository>();

        cfg.UseMessageData(messageDataRepository);

        cfg.Host(new Uri(connectionString));

        cfg.ConfigureEndpoints(context);
      });
    });

    return services;
  }
}