using Huntask.Assets.Service.Application.Models;

namespace Huntask.Assets.Service.Presentation.Models;

public record ApiResult<T>(
  bool Success,
  T? Data = default,
  List<ApiLink>? Links = null,
  string[] Errors = null!)
{
  public ApiResult(Result<T> result)
    : this(result.Success, result.Data, [], result.Errors)
  {
  }

  public List<ApiLink> Links { get; init; } = Links ?? [];
}
