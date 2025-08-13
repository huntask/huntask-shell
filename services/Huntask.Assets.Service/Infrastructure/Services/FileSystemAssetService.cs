
using Huntask.Assets.Service.Domain.Services;
using Huntask.Assets.Service.Infrastructure.Models.Options;
using Microsoft.Extensions.Options;

namespace Huntask.Assets.Service.Infrastructure.Services;

public class FileSystemAssetService(IOptionsSnapshot<AssetsOptions> assetsOptionsSnapshot) : IAssetService
{
  private readonly AssetsOptions options = assetsOptionsSnapshot.Value;

  public async Task UploadAsync(
    string fileName,
    string extension,
    Stream stream,
    string containerName)
  {
    ArgumentException.ThrowIfNullOrEmpty(fileName, nameof(fileName));
    ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));

    var containerPath = Path.Combine(options.Folder, containerName ?? options.DefaultContainer);
    if (!Directory.Exists(containerPath))
    {
      Directory.CreateDirectory(containerPath);
    }

    var filePath = Path.Combine(containerPath, $"{fileName}{extension}");
    await using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    await stream.CopyToAsync(file);
  }

  public FileStream Download(string fileName, string extension, string containerName)
  {
    ArgumentException.ThrowIfNullOrEmpty(fileName, nameof(fileName));
    ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));

    extension = NormalizeExtension(extension);
    ThrowIfExtensionForbidden(extension);

    var filePath = Path.Combine(
      options.Folder,
      containerName ?? options.DefaultContainer,
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
      Common.Constants.MaxFileSize,
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
      options.Folder,
      containerName ?? options.DefaultContainer,
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
