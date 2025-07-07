using Huntask.Identity.Service.Application.Models;

namespace Huntask.Identity.Service.Presentation.Models;

public record ApiResult<T>(
  bool Success,
  T? Data = default,
  params string[] Errors)
{
  public ApiResult(Result<T> result) : this(result.Success, result.Data, result.Errors) {}
}
