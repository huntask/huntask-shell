using System.Reflection;
using CorrelationId;
using Huntask.Common.Presentation.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Huntask.Common.Presentation.Extensions;

public static class WebApplicationDecorationsExtensions
{
  public static WebApplication ConfigureCommonDecorations(this WebApplication app)
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
        var documentName = Assembly.GetExecutingAssembly().GetName().Name;

        foreach (var description in provider.ApiVersionDescriptions)
        {
          c.SwaggerEndpoint(
            $"./{description.GroupName}/swagger.json",
            $"{documentName} {description.GroupName}"
          );
          c.RoutePrefix = "swagger";
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