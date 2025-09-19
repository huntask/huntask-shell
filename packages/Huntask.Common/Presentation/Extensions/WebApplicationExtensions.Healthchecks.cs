using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Huntask.Common.Presentation.Extensions;

public static class WebApplicationHealthchecksExtensions
{
  public static WebApplication ConfigureHealthChecks(this WebApplication app)
  {
    app.MapHealthChecks("/healthz", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    return app;
  }
}