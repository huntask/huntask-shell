namespace Huntask.Identity.Service.Presentation.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
  private readonly RequestDelegate next = next;

  public async Task InvokeAsync(HttpContext context)
  {
    if (!context.Request.Headers.TryGetValue(Constants.CorrelationIdHeaderName, out var correlationId))
    {
      correlationId = Guid.NewGuid().ToString();
    }

    context.TraceIdentifier = correlationId!;
    context.Response.Headers[Constants.CorrelationIdHeaderName] = correlationId;

    await next(context);
  }
}