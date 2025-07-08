using Huntask.Identity.Service.Application.Commands.RegisterUserCommand;
using MediatR;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class ServiceCollectionPresentationExtensions
{
  public static IServiceCollection AddPresentationServices(this IServiceCollection services)
  {
    services
      .AddApiControllers()
      .AddApiVersioning()
      .AddMediatR();

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

  private static IServiceCollection AddMediatR(this IServiceCollection services)
  {
    services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
    return services;
  }
}