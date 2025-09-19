using Huntask.Assets.Service.Application.Commands.UploadAsset;
using Huntask.Assets.Service.Application.Commands.DeleteAsset;
using Huntask.Assets.Service.Presentation.Models;
using Huntask.Assets.Service.Application.Queries.GetAsset;
using Huntask.Assets.Service.Application.Queries.DownloadAsset;
using Huntask.Common.Presentation.Controllers;
using Huntask.Assets.Service.Domain.Models;
using static Huntask.Common.Constants;
using Huntask.Common.Application.Extensions;

namespace Huntask.Assets.Service.Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/asset")]
public class AssetsApiController(IMediator mediator, ILogger<AssetsApiController> logger) : BaseApiController
{
  [HttpGet("{id}")]
  public async Task<IActionResult> GetAssetAsync([FromRoute] string id)
  {
    var result = await mediator.Send(new GetAssetQuery(id));
    return result.ToActionResult();
  }

  [HttpGet("{id}/download")]
  public async Task<IActionResult> DownloadAssetAsync([FromRoute] string id)
  {
    var result = await mediator.Send(new DownloadAssetQuery(id));

    var data = result.Value;
    var asset = data?.Asset;
    var stream = data?.Stream;

    if (stream is null)
    {
      var response = result
        .WithError(new Error("Stream is null")
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.StreamIsNull))
        .LogErrors(logger, "Failed to read stream");
      return new ObjectResult(response);
    }

    if (asset is null)
    {
      var response = result
        .WithError(new Error("Asset is null")
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.AssetIsNull))
        .LogErrors(logger, "Asset appeared to be null");
      return new ObjectResult(response);
    }

    if (!result.IsSuccess)
    {
      var response = result.LogErrors(logger, "The result was not successful");
      return new ObjectResult(response);
    }

    return File(stream, asset.ContentType, $"{asset.OriginalName}{asset.Extension}");
  }

  [HttpGet("{id}/view")]
  public async Task<IActionResult> ViewAssetAsync([FromRoute] string id)
  {
    var result = await mediator.Send(new DownloadAssetQuery(id));

    var data = result.Value;
    var asset = data?.Asset;
    var stream = data?.Stream;

    if (stream is null)
    {
      var response = result
        .WithError(new Error("Stream is null")
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.StreamIsNull))
        .LogErrors(logger, "Failed to read stream");
      return new ObjectResult(response);
    }

    if (asset is null)
    {
      var response = result
        .WithError(new Error("Asset is null")
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.AssetIsNull))
        .LogErrors(logger, "Asset appeared to be null");
      return new ObjectResult(response);
    }

    if (!result.IsSuccess)
    {
      var response = result.LogErrors(logger, "The result was not successful");
      return new ObjectResult(response);
    }

    var fileFullName = $"{asset.OriginalName}{asset.Extension}";
    Response.Headers.ContentDisposition = $"inline; filename=\"{fileFullName}\"";
    return File(stream, asset.ContentType, fileFullName);
  }

  [HttpPost]
  [Consumes("multipart/form-data")]
  public async Task<Result<Asset>> UploadAssetAsync([FromForm] UploadFileRequest request)
  {
    var model = new UploadAssetModel(
      request.File.OpenReadStream(),
      request.File.FileName,
      request.File.ContentType,
      request.ContainerName!
    );
    return await mediator.Send(new UploadAssetCommand(model));
  }

  [HttpDelete("{id}")]
  public async Task<Result<Asset>> DeleteAssetAsync([FromRoute] string id)
  {
    return await mediator.Send(new DeleteAssetCommand(id));
  }
}
