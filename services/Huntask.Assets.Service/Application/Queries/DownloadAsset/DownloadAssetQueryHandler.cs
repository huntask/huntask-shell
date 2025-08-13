using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Domain.Services;
using Huntask.Common.Application.Models;

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
        return new Result<DownloadAssetModel>(false, HttpStatusCode.NotFound, null, [$"Asset {id} not found."]);
      }
      var model = new DownloadAssetModel
      {
        Asset = asset,
        Stream = assetService.Download(asset.InternalName, asset.Extension, asset.ContainerName)
      };

      return new Result<DownloadAssetModel>(true, HttpStatusCode.OK, model, []);
    }
    catch (Exception ex)
    {
      return new Result<DownloadAssetModel>(false, HttpStatusCode.InternalServerError, null, [ex.Message]);
    }
  }
}
