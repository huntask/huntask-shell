namespace Huntask.Assets.Service.Domain.Services;

public interface IAssetService
{
  public Task UploadAsync(string fileName, string extension, Stream stream, string containerName);
  public FileStream Download(string fileName, string extension, string containerName);
  public bool Delete(string fileName, string extension, string containerName);
}
