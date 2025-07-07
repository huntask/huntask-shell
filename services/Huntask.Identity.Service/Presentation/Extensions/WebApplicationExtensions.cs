using CorrelationId;
using Huntask.Identity.Service.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Huntask.Identity.Service.Presentation.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class WebApplicationExtensions
{
  public static void ConfigureWebApplication(this WebApplication app)
  {
    app
      .MapWebApiControllers()
      .ConfigureSwagger()
      .ConfigureDatabase()
      .ConfigureHttps()
      .ConfigureMiddleware()
      .ConfigureCache()
      .UseCorrelationId();
  }

  private static WebApplication MapWebApiControllers(this WebApplication app)
  {
    app.MapControllers();

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

  private static WebApplication ConfigureDatabase(this WebApplication app)
  {
    try
    {
      using (var scope = app.Services.CreateScope())
      {
        var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        db.Database.Migrate();
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error during database migration: {ex.Message}");
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