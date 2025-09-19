using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Domain.Services;
using static Huntask.Common.Constants;

namespace Huntask.Assets.Service.Application.Commands.UploadAsset;

public class UploadAssetCommandHandler(IAssetService assetService) : IRequestHandler<UploadAssetCommand, Result<Asset>>
{
  public async Task<Result<Asset>> Handle(UploadAssetCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var model = request.Model;

      if (model.FileStream == null || model.FileStream.Length == 0)
      {
        return Result.Fail(new Error("File stream is empty or null.")
          .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.FileStreamIsEmptyOrNull));
      }
      var asset = await assetService.UploadAsync(model);

      return Result.Ok(asset);
    }
    catch (Exception ex)
    {
      return Result.Fail(new Error(ex.Message)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.FileStreamIsEmptyOrNull));
    }
  }
}
