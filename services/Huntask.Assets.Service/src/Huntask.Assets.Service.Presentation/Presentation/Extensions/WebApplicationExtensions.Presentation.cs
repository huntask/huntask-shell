using Huntask.Assets.Service.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Huntask.Assets.Service.Presentation.Extensions;

[ExcludeFromCodeCoverage]
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
      var db = scope.ServiceProvider.GetRequiredService<AssetsContext>();
      db.Database.Migrate();
    }
    catch (Exception ex)
    {
      Log.Error(Common.Constants.LogTemplate, $"Error during database migration: {ex.Message}");
    }

    return app;
  }
}