using Huntask.Common.Presentation.Models;
using Microsoft.AspNetCore.Http;

namespace Huntask.Common.Presentation.Middlewares;

public class ExceptionsMiddleware(RequestDelegate next)
{
  private readonly RequestDelegate next = next;

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;

      var response = new ApiResult<object>(
        Success: false,
        Data: default,
        Links: [],
        Errors: [ex.Message]
      );

      var json = JsonSerializer.Serialize(response);
      await context.Response.WriteAsync(json);
    }
  }
}
