using Huntask.Assets.Service.Infrastructure.Models.Options;
using Huntask.Common.Presentation.Extensions;

namespace Huntask.Assets.Service.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
  {
    builder
      .ConfigureCommonBuilder()
      .ConfigureOptions()
      .ConfigureKestrel();

    return builder;
  }

  private static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
  {
    if (builder.Environment.IsDevelopment())
    {
      builder.WebHost.ConfigureKestrel(options =>
      {
        options.ListenAnyIP(builder.Configuration.GetValue("HUNTASK_ASSETS_SERVICE_HTTP_PORT", 8082));
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