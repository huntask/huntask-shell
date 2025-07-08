using CorrelationId;
using Huntask.Identity.Service.Presentation.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class WebApplicationDecorationsExtensions
{
  public static WebApplication ConfigureDecorations(this WebApplication app)
  {
    app
      .ConfigureSwagger()
      .ConfigureHttps()
      .ConfigureMiddleware()
      .ConfigureCache()
      .UseCorrelationId();

    return app;
  }

  private static WebApplication ConfigureSwagger(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
          c.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            $"Huntask.Identity.Api {description.GroupName}"
          );
        }
      });
    }

    return app;
  }

  private static WebApplication ConfigureHttps(this WebApplication app)
  {
    app.UseHttpsRedirection();
    return app;
  }

  private static WebApplication ConfigureMiddleware(this WebApplication app)
  {
    app.UseMiddleware<ExceptionsMiddleware>();
    return app;
  }

  private static WebApplication ConfigureCache(this WebApplication app)
  {
    app.UseOutputCache();
    app.UseResponseCaching();

    return app;
  }
}