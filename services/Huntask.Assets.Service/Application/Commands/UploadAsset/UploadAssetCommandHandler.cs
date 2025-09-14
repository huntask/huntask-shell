using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Services;
using Huntask.Common.Application.Models;

namespace Huntask.Assets.Service.Application.Commands.UploadAsset;

public class UploadAssetCommandHandler(IAssetService assetService) : IRequestHandler<UploadAssetCommand, Result<Asset?>>
{
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
      var asset = await assetService.UploadAsync(model);

      return new Result<Asset?>(true, HttpStatusCode.OK, asset);
    }
    catch (Exception ex)
    {
      return new Result<Asset?>(false, HttpStatusCode.InternalServerError, null, [ex.Message]);
    }
  }
}
