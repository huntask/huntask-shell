using Huntask.Assets.Service.Application.Models;
using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Repositories;
using Huntask.Assets.Service.Domain.Services;

namespace Huntask.Assets.Service.Application.Commands.UploadAsset;

public class UploadAssetCommandHandler : IRequestHandler<UploadAssetCommand, Result<Asset?>>
{
  private readonly IAssetService assetService;
  private readonly IAssetRepository assetRepository;

  public UploadAssetCommandHandler(
    IAssetService assetService,
    IAssetRepository assetRepository)
  {
    ArgumentNullException.ThrowIfNull(assetService, nameof(assetService));
    ArgumentNullException.ThrowIfNull(assetRepository, nameof(assetRepository));

    this.assetService = assetService;
    this.assetRepository = assetRepository;
  }

  public async Task<Result<Asset?>> Handle(UploadAssetCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var model = request.Model;

      if (model.FileStream == null || model.FileStream.Length == 0)
      {
        return new Result<Asset?>(
          false,
          HttpStatusCode.BadRequest,
          null,
          ["File stream is empty or null."]
        );
      }

      var createdAt = DateTime.UtcNow;
      var id = Guid.NewGuid().ToString();
      var asset = new Asset
      {
        Id = id,
        ContainerName = model.ContainerName,
        OriginalName = Path.GetFileNameWithoutExtension(model.FileName),
        InternalName = id,
        Extension = Path.GetExtension(model.FileName),
        ContentType = model.ContentType,
        Size = model.FileStream.Length,
        CreatedAt = createdAt,
        UpdatedAt = createdAt
      };
      await assetRepository.CreateAsync(asset);

      await assetService.UploadAsync(
        asset.InternalName,
        asset.Extension,
        model.FileStream,
        asset.ContainerName
      );

      return new Result<Asset?>(true, HttpStatusCode.OK, asset);
    }
    catch (Exception ex)
    {
      return new Result<Asset?>(false, HttpStatusCode.InternalServerError, null, [ex.Message]);
    }
  }
}
