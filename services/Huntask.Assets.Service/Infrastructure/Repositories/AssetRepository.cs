using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Infrastructure.Contexts;

namespace Huntask.Assets.Service.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
  private const string AssetNotFoundException = "Asset not found";

  private readonly AssetsContext db;

  public AssetRepository(AssetsContext db)
  {
    ArgumentNullException.ThrowIfNull(db, nameof(db));
    this.db = db;
  }

  public async Task<Asset> CreateAsync(Asset asset)
  {
    db.Assets.Add(asset);
    await db.SaveChangesAsync();
    return asset;
  }

  public async Task<Asset> DeleteAsync(string id)
  {
    var asset = await db.Assets.FindAsync(id)
      ?? throw new InvalidOperationException(AssetNotFoundException);
    db.Assets.Remove(asset);
    await db.SaveChangesAsync();
    return asset;
  }

  public async Task<Asset?> GetAsync(string id)
  {
    return await db.Assets.FindAsync(id);
  }

  public async Task<Asset> UpdateAsync(string id, Asset asset)
  {
    var existingAsset = await db.Assets.FindAsync(id)
      ?? throw new InvalidOperationException(AssetNotFoundException);

    existingAsset.OriginalName = asset.OriginalName;
    existingAsset.InternalName = asset.InternalName;
    existingAsset.Extension = asset.Extension;
    existingAsset.ContentType = asset.ContentType;
    existingAsset.Size = asset.Size;
    existingAsset.ContainerName = asset.ContainerName;
    existingAsset.UpdatedAt = DateTime.UtcNow;

    await db.SaveChangesAsync();

    return existingAsset;
  }
}
