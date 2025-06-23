using Microsoft.EntityFrameworkCore;
using Identity.Service.Presentation.Middlewares;
using Identity.Service.Infrastructure.Contexts;

namespace Identity.Service.Presentation.Extensions;

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
      var documentPath = "/todolist/swagger/{documentName}/swagger.json";

      app.UseOpenApi(config =>
      {
        config.Path = documentPath;
      });

      app.UseSwaggerUi(config =>
      {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/todolist/swagger";
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
    using (var scope = app.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<TodoListContext>();
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