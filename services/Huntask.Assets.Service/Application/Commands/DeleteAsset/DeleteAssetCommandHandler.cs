using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Domain.Services;
using Huntask.Common.Application.Models;

namespace Huntask.Assets.Service.Application.Commands.DeleteAsset;

public class DeleteAssetCommandHandler(
    IAssetService assetService,
    IAssetRepository assetRepository) : IRequestHandler<DeleteAssetCommand, Result<Asset>>
{
  public async Task<Result<Asset>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var id = request.Id;

      var asset = await assetRepository.DeleteAsync(id);

      var success = assetService.Delete(
        asset.InternalName,
        asset.Extension,
        asset.ContainerName
      );

      var result = new Result<Asset>(
        success,
        success ? HttpStatusCode.OK : HttpStatusCode.NotFound,
        asset,
        success ? [] : [$"Asset {asset.InternalName} not found in container {asset.ContainerName}."]
      );

      return result;
    }
    catch (Exception ex)
    {
      return new Result<Asset>(false, HttpStatusCode.InternalServerError, null, [ex.Message]);
    }
  }
}
