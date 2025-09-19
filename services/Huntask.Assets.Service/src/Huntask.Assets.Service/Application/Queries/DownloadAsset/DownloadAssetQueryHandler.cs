using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Domain.Services;
using static Huntask.Common.Constants;

namespace Huntask.Assets.Service.Application.Queries.DownloadAsset;

public class DownloadAssetQueryHandler(
  IAssetService assetService,
  IAssetRepository assetRepository) : IRequestHandler<DownloadAssetQuery, Result<DownloadAssetModel>>
{

  public async Task<Result<DownloadAssetModel>> Handle(DownloadAssetQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var id = request.Id;

      var asset = await assetRepository.GetAsync(id);

      if (asset == null)
      {
        return Result.Fail(new Error($"Asset {id} not found.")
          .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.AssetNotFound));
      }
      var model = new DownloadAssetModel
      {
        Asset = asset,
        Stream = assetService.Download(asset.InternalName, asset.Extension, asset.ContainerName)
      };

      return Result.Ok(model);
    }
    catch (Exception ex)
    {
      return Result.Fail(new Error(ex.Message)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.InternalServerError));
    }
  }
}
