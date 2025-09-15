using Huntask.Assets.Service.Domain.Models;

namespace Huntask.Assets.Service.Domain.Repositories;

public interface IAssetRepository
{
  public Task<Asset?> GetAsync(string id);
  public Task<Asset> CreateAsync(Asset asset);
  public Task<Asset> UpdateAsync(string id, Asset asset);
  public Task<Asset> DeleteAsync(string id);
}
