using CorrelationId;
using Identity.Service.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Identity.Service.Presentation.Middlewares;

namespace Identity.Service.Presentation.Extensions;

public static class WebApplicationExtensions
{
  public static void ConfigureWebApplication(this WebApplication app)
  {
    app
      .ConfigureSwagger()
      .ConfigureDatabase()
      .ConfigureHttps()
      .ConfigureMiddleware()
      .ConfigureCache()
      .UseCorrelationId();
  }

  private static WebApplication ConfigureSwagger(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      var documentPath = "/identity/swagger/{documentName}/swagger.json";
      app.UseOpenApi(config =>
      {
        config.Path = documentPath;
      });

      app.UseSwaggerUi(config =>
      {
        config.DocumentTitle = "IdentityAPI";
        config.Path = "/identity/swagger";
        config.DocumentPath = documentPath;
        config.DocExpansion = "list";
      });
    }

    return app;
  }

  private static WebApplication ConfigureDatabase(this WebApplication app)
  {
    using (var scope = app.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
      db.Database.Migrate();
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