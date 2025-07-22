using Huntask.Common.Presentation.Extensions;
using Huntask.Identity.Service.Domain.Models;
using Huntask.Identity.Service.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
  {
    builder
      .ConfigureCommonBuilder()
      .ConfigureAuth()
      .ConfigureKestrel();

    return builder;
  }

  private static WebApplicationBuilder ConfigureAuth(this WebApplicationBuilder builder)
  {
    builder.Services.AddIdentity<User, IdentityRole>()
      .AddEntityFrameworkStores<IdentityContext>()
      .AddDefaultTokenProviders();

    return builder;
  }

  private static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
  {
    if (builder.Environment.IsDevelopment())
    {
      builder.WebHost.ConfigureKestrel(options =>
      {
        options.ListenAnyIP(builder.Configuration.GetValue("HUNTASK_IDENTITY_SERVICE_HTTP_PORT", 8081));
      });
    }

    return builder;
  }
}