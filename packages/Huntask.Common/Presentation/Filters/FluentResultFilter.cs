using Huntask.Common.Application.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Huntask.Common.Presentation.Filters;

public class FluentResultFilter : IAsyncResultFilter
{
  public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
  {
    if (context.Result is ObjectResult objectResult && objectResult.Value is IResultBase result)
    {
      context.Result = result switch
      {
        Result nonGenericResult => nonGenericResult.ToActionResult(),
        _ => (IActionResult)((dynamic)result).ToActionResult(),
      };
    }

    await next();
  }
}