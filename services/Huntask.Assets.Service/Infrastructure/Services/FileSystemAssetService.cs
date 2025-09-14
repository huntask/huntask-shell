
using Huntask.Assets.Service.Application.Commands.UploadAsset;
using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Domain.Services;
using Huntask.Assets.Service.Infrastructure.Models.Options;
using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.Extensions.Options;

namespace Huntask.Assets.Service.Infrastructure.Services;

public class FileSystemAssetService(
  IOptionsSnapshot<AssetsOptions> assetsOptionsSnapshot,
  IOptionsSnapshot<FileSizesOptions> fileSizesOptionsSnapshot,
  IAssetRepository assetRepository) : IAssetService
{
  private readonly AssetsOptions assetsOptions = assetsOptionsSnapshot.Value;
  private readonly FileSizesOptions fileSizesOptions = fileSizesOptionsSnapshot.Value;

  public async Task<Asset> UploadAsync(UploadAssetModel model)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));

    var createdAt = DateTime.UtcNow;
    var id = Guid.NewGuid().ToString();
    var asset = new Asset
    {
      Id = id,
      ContainerName = model.ContainerName,
      OriginalName = Path.GetFileNameWithoutExtension(model.FileName),
      InternalName = id,
      Extension = Path.GetExtension(model.FileName),
      ContentType = model.ContentType,
      Size = model.FileStream.Length,
      CreatedAt = createdAt,
      UpdatedAt = createdAt
    };
    await assetRepository.CreateAsync(asset);

    var containerPath = Path.Combine(assetsOptions.Folder, model.ContainerName ?? assetsOptions.DefaultContainer);
    if (!Directory.Exists(containerPath))
    {
      Directory.CreateDirectory(containerPath);
    }

    var filePath = Path.Combine(containerPath, $"{asset.InternalName}{asset.Extension}");
    await using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    await model.FileStream.CopyToAsync(file);

    return asset;
  }

  public FileStream Download(string fileName, string extension, string containerName)
  {
    ArgumentException.ThrowIfNullOrEmpty(fileName, nameof(fileName));
    ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));

    extension = NormalizeExtension(extension);
    ThrowIfExtensionForbidden(extension);

    var filePath = Path.Combine(
      assetsOptions.Folder,
      containerName ?? assetsOptions.DefaultContainer,
      $"{fileName}{extension}"
    );

    if (!File.Exists(filePath))
    {
      throw new FileNotFoundException("Asset not found", filePath);
    }

    return new(filePath,
      FileMode.Open,
      FileAccess.Read,
      FileShare.Read,
      fileSizesOptions.MaxAssetFileSize,
      useAsync: true
    );
  }

  public bool Delete(string fileName, string extension, string containerName)
  {
    ArgumentException.ThrowIfNullOrEmpty(fileName, nameof(fileName));
    ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));

    extension = NormalizeExtension(extension);
    ThrowIfExtensionForbidden(extension);

    var filePath = Path.Combine(
      assetsOptions.Folder,
      containerName ?? assetsOptions.DefaultContainer,
      $"{fileName}{extension}"
    );

    if (File.Exists(filePath))
    {
      File.Delete(filePath);
      return true;
    }

    return false;
  }

  private static string NormalizeExtension(string extension)
  {
    if (!extension.StartsWith('.'))
    {
      return "." + extension.ToLowerInvariant();
    }

    return extension.ToLowerInvariant();
  }

  private static string ThrowIfExtensionForbidden(string extension)
  {
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".txt" };
    if (allowedExtensions.Contains(extension))
    {
      return extension;
    }

    throw new NotSupportedException($"The file extension '{extension}' is not supported.");
  }
}
