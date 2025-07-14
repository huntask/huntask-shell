namespace Huntask.Assets.Service.Presentation.Endpoints;

public static class HealthcheckEndpoint
{
  public static WebApplication RegisterHealthcheckEndpoint(this WebApplication app)
  {
    app
      .MapGet("api/healthcheck", () => Results.Ok("Assets Service is running"))
      .WithName("Healthcheck")
      .WithTags("Healthcheck");

    return app;
  }
}
