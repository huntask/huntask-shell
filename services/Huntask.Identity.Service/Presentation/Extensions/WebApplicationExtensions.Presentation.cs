using Huntask.Identity.Service.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Huntask.Identity.Service.Presentation.Extensions;

public static class WebApplicationPresentationExtensions
{
  public static WebApplication ConfigurePresentation(this WebApplication app)
  {
    app
      .MapApiControllers()
      .ConfigureDatabase();

    return app;
  }

  private static WebApplication MapApiControllers(this WebApplication app)
  {
    app.MapControllers();

    return app;
  }

  private static WebApplication ConfigureDatabase(this WebApplication app)
  {
    try
    {
      using var scope = app.Services.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
      db.Database.Migrate();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error during database migration: {ex.Message}");
    }

    return app;
  }
}