namespace Huntask.Assets.Service.Domain.Models;

public class Asset
{
  public string Id { get; set; } = null!;
  public string ContainerName { get; set; } = "";
  public string OriginalName { get; set; } = "";
  public string InternalName { get; set; } = "";
  public string Extension { get; set; } = "";
  public string ContentType { get; set; } = "";
  public long Size { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
