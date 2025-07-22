using Microsoft.AspNetCore.Http;

namespace Huntask.Common.Presentation.Models;

public class UploadFileRequest
{
  public IFormFile File { get; set; } = null!;
  public string? ContainerName { get; set; }
}
