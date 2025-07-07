namespace Huntask.Identity.Service.Presentation.Models;

public record ApiResponse<T>(
  bool Success,
  T? Data = default,
  params string[] Errors
);
