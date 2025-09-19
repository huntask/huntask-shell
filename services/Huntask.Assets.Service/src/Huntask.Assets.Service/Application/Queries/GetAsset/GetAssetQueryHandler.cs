using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using static Huntask.Common.Constants;

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
        return Result.Fail(new Error($"Asset {id} not found.")
          .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.AssetNotFound));
      }

      return Result.Ok(asset);
    }
    catch (Exception ex)
    {
      return Result.Fail(new Error(ex.Message)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.InternalServerError));
    }
  }
}