namespace Huntask.Assets.Service.Presentation.Models;

public class UploadFileRequest
{
  public IFormFile File { get; set; } = null!;
  public string? ContainerName { get; set; }
}
