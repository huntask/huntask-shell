using Microsoft.EntityFrameworkCore;
using Huntask.Identity.Service.Presentation.Middlewares;
using Huntask.Identity.Service.Infrastructure.Contexts;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class WebApplicationExtensions
{
  public static void ConfigureWebApplication(this WebApplication app)
  {
    app
      .ConfigureSwagger()
      .ConfigureAuth()
      .ConfigureDatabase()
      .ConfigureHttps()
      .ConfigureMiddleware()
      .ConfigureCache();
  }

  private static WebApplication ConfigureSwagger(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      var documentPath = "/swagger/{documentName}/swagger.json";

      app.UseOpenApi(config =>
      {
        config.Path = documentPath;
      });

      app.UseSwaggerUi(config =>
      {
        config.DocumentTitle = "Huntask.Tasks.Api";
        config.Path = "/swagger";
        config.DocumentPath = documentPath;
        config.DocExpansion = "list";
      });
    }

    return app;
  }

  private static WebApplication ConfigureAuth(this WebApplication app)
  {
    app.UseAuthentication();
    app.UseAuthorization();

    return app;
  }

  private static WebApplication ConfigureDatabase(this WebApplication app)
  {
    try
    {
      using (var scope = app.Services.CreateScope())
      {
        var db = scope.ServiceProvider.GetRequiredService<TasksContext>();
        db.Database.Migrate();
      }
    }
    catch (Exception ex)
    {
      Log.Error($"Error during database migration: {ex.Message}");
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
    app.UseMiddleware<CorrelationIdMiddleware>();
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