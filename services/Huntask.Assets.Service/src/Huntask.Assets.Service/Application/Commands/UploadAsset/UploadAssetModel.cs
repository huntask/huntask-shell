namespace Huntask.Assets.Service.Application.Commands.UploadAsset;

public record UploadAssetModel(
  Stream FileStream,
  string FileName,
  string ContentType,
  string ContainerName
);
