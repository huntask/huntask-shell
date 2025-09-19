using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Domain.Services;
using static Huntask.Common.Constants;

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

      return success
        ? Result.Ok(asset)
        : Result.Fail(new Error($"Asset {asset.InternalName} not found in container {asset.ContainerName}.")
          .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.AssetNotFound));
    }
    catch (Exception ex)
    {
      return Result.Fail(new Error(ex.Message)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.InternalServerError));
    }
  }
}
