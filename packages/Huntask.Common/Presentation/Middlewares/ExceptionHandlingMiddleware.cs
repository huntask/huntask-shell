using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Huntask.Common.Presentation.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An unhandled exception occurred while processing the request.");
      await WriteProblemDetailsResponseAsync(context, ex);
    }
  }

  private static async Task WriteProblemDetailsResponseAsync(HttpContext context, Exception exception)
  {
    var problemDetails = new ProblemDetails
    {
      Type = "https://httpstatuses.com/500",
      Title = "An unexpected error occurred.",
      Status = StatusCodes.Status500InternalServerError,
      Detail = exception.Message,
      Instance = context.Request.Path
    };

    context.Response.ContentType = "application/problem+json";
    context.Response.StatusCode = problemDetails.Status.Value;
    await context.Response.WriteAsJsonAsync(problemDetails);
  }
}