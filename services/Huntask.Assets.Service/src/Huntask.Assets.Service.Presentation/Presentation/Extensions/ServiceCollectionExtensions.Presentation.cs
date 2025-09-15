using Huntask.Common.Presentation.Extensions;

namespace Huntask.Assets.Service.Presentation.Extensions;

[ExcludeFromCodeCoverage]
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
    services.AddMediatR(typeof(Huntask.Assets.Service.AssemblyReference).Assembly);
    return services;
  }
}