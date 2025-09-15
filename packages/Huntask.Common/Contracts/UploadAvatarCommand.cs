using MassTransit;

namespace Huntask.Common.Contracts;

public interface UploadAvatarCommand
{
  public string UserId { get; }
  public string FileName { get; }
  public string ContentType { get; }
  public string ContainerName { get; }
  public MessageData<Stream> Avatar { get; }
}