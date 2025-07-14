using Huntask.Assets.Service.Application.Models;
using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Domain.Services;

namespace Huntask.Assets.Service.Application.Commands.DeleteAsset;

public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, Result<Asset>>
{
  private readonly IAssetService assetService;
  private readonly IAssetRepository assetRepository;

  public DeleteAssetCommandHandler(
    IAssetService assetService,
    IAssetRepository assetRepository)
  {
    ArgumentNullException.ThrowIfNull(assetService, nameof(assetService));
    ArgumentNullException.ThrowIfNull(assetRepository, nameof(assetRepository));

    this.assetService = assetService;
    this.assetRepository = assetRepository;
  }

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
