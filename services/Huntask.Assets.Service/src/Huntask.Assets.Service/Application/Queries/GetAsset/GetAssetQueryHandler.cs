using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Common.Application.Models;

namespace Huntask.Assets.Service.Application.Queries.GetAsset;

public class GetAssetQueryHandler : IRequestHandler<GetAssetQuery, Result<Asset>>
{
  private readonly IAssetRepository assetRepository;

  public GetAssetQueryHandler(
    IAssetRepository assetRepository)
  {
    ArgumentNullException.ThrowIfNull(assetRepository, nameof(assetRepository));

    this.assetRepository = assetRepository;
  }

  public async Task<Result<Asset>> Handle(GetAssetQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var id = request.Id;

      var asset = await assetRepository.GetAsync(id);

      if (asset == null)
      {
        return new Result<Asset>(false, HttpStatusCode.NotFound, null, [$"Asset {id} not found."]);
      }

      return new Result<Asset>(true, HttpStatusCode.OK, asset, []);
    }
    catch (Exception ex)
    {
      return new Result<Asset>(false, HttpStatusCode.InternalServerError, null, [ex.Message]);
    }
  }
}