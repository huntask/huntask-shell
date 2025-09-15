namespace Huntask.Common.Contracts;

public interface AvatarUploadedEvent
{
  public string UserId { get; }
  public string AssetId { get; }
}