
namespace Huntask.Assets.Service.Application.Models;

public record Result<T>(
  bool Success,
  HttpStatusCode StatusCode,
  T? Data = default,
  params string[] Errors
);
