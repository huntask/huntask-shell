using System.Net;
using Huntask.Identity.Service.Presentation.Models;

namespace Huntask.Identity.Service.Application.Models;

public record Result<T>(
  bool Success,
  HttpStatusCode StatusCode,
  T? Data = default,
  params string[] Errors
);
