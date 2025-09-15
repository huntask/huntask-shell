using Huntask.Common.Presentation.Extensions;
using Huntask.Identity.Service.Domain.Models;
using Huntask.Identity.Service.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Huntask.Identity.Service.Presentation.Extensions;

[ExcludeFromCodeCoverage]
public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
  {
    builder
      .ConfigureCommonBuilder()
      .ConfigureAuth()
      .ConfigureKestrel()
      .AddDatabaseDeveloperPageExceptionFilter();

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
        options.ListenAnyIP(builder.Configuration.GetValue("HuntaskPorts__IdentityServiceHttp", 8081));
      });
    }

    return builder;
  }

  private static WebApplicationBuilder AddDatabaseDeveloperPageExceptionFilter(this WebApplicationBuilder builder)
  {
    if (builder.Environment.IsDevelopment())
    {
      builder
        .Services
        .AddDatabaseDeveloperPageExceptionFilter();
    }

    return builder;
  }
}