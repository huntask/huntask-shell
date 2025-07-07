using Microsoft.AspNetCore.Http;

namespace Huntask.Identity.Service.Presentation.Middlewares;

public class CorrelationIdMiddleware
{
  private const string HeaderName = "X-Correlation-Id";

  private readonly RequestDelegate next;

  public CorrelationIdMiddleware(RequestDelegate next)
  {
    this.next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    if (!context.Request.Headers.TryGetValue(HeaderName, out var correlationId))
    {
      correlationId = Guid.NewGuid().ToString();
    }

    context.TraceIdentifier = correlationId!;
    context.Response.Headers[HeaderName] = correlationId;

    await next(context);
  }
}