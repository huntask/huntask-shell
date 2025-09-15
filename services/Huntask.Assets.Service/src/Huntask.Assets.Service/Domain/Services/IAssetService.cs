using Huntask.Assets.Service.Application.Commands.UploadAsset;
using Huntask.Assets.Service.Domain.Models;

namespace Huntask.Assets.Service.Domain.Services;

public interface IAssetService
{
  public Task<Asset> UploadAsync(UploadAssetModel model);
  public FileStream Download(string fileName, string extension, string containerName);
  public bool Delete(string fileName, string extension, string containerName);
}
