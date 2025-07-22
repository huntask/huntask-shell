using Huntask.Assets.Service.Application.Commands.UploadAsset;
using Huntask.Common.Presentation.Extensions;

namespace Huntask.Assets.Service.Presentation.Extensions;

public static class ServiceCollectionPresentationExtensions
{
  public static IServiceCollection AddPresentationServices(this IServiceCollection services)
  {
    services
      .AddCommonPresentationServices()
      .AddMediatR();

    return services;
  }

  private static IServiceCollection AddMediatR(this IServiceCollection services)
  {
    services.AddMediatR(typeof(UploadAssetCommandHandler).Assembly);
    return services;
  }
}