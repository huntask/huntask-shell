using System.Text.Json;
using Huntask.Identity.Service.Presentation.Models;

namespace Huntask.Identity.Service.Presentation.Middlewares;

public class ExceptionsMiddleware
{
  private readonly RequestDelegate next;

  public ExceptionsMiddleware(RequestDelegate next)
  {
    this.next = next;
  }

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
        Errors: [ex.Message]
      );

      var json = JsonSerializer.Serialize(response);
      await context.Response.WriteAsync(json);
    }
  }
}
