using Microsoft.AspNetCore.Mvc;

using Huntask.Assets.Service.Application.Commands.UploadAsset;
using Huntask.Assets.Service.Application.Commands.DeleteAsset;
using Huntask.Assets.Service.Presentation.Models;
using Huntask.Assets.Service.Domain.Models;
using Huntask.Assets.Service.Application.Queries.GetAsset;
using Huntask.Assets.Service.Application.Queries.DownloadAsset;

namespace Huntask.Assets.Service.Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/asset")]
public class AssetsApiController(IMediator mediator) : BaseApiController
{
  public const int MaxFileSize = 50 * 1024 * 1024;

  [HttpGet("{id}")]
  public async Task<IActionResult> GetAssetAsync([FromRoute] string id)
  {
    var result = await mediator.Send(new GetAssetQuery(id));

    return result.Success
      ? StatusCode((int)result.StatusCode, new ApiResult<Asset>(
          result.Success,
          result.Data,
          [
            new ApiLink
            {
              Rel = "file",
              Href = $"{HateoasBaseUrl}asset/{id}/download",
              Method = "GET"
            },
          ]
        ))
      : FromResult(result);
  }

  [HttpGet("{id}/download")]
  public async Task<IActionResult> DownloadAssetAsync([FromRoute] string id)
  {
    var result = await mediator.Send(new DownloadAssetQuery(id));

    var data = result.Data;
    var asset = data?.Asset;
    var stream = data?.Stream;

    if (!result.Success || stream == null || asset == null)
    {
      return FromResult(result);
    }

    return File(stream, asset.ContentType, $"{asset.OriginalName}{asset.Extension}");
  }

  [HttpPost]
  [RequestSizeLimit(MaxFileSize)]
  public async Task<IActionResult> UploadAssetAsync([FromForm] UploadFileRequest request)
  {
    var model = new UploadAssetModel(
      request.File.OpenReadStream(),
      request.File.FileName,
      request.File.ContentType,
      request.ContainerName!
    );
    var result = await mediator.Send(new UploadAssetCommand(model));
    var id = result.Data?.Id;
    var links = new List<ApiLink>();
    if (id != null)
    {
      links.AddRange([
        new ApiLink
        {
          Rel = "self",
          Href = $"{HateoasBaseUrl}asset/{result.Data?.Id}",
          Method = "GET"
        },
        new ApiLink
        {
          Rel = "file",
          Href = $"{HateoasBaseUrl}asset/{id}/download",
          Method = "GET"
        },
      ]);
    }

    return result.Success
      ? StatusCode((int)result.StatusCode, new ApiResult<Asset?>(
          result.Success,
          result.Data,
          links
        ))
      : FromResult(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteAssetAsync([FromRoute] string id)
  {
    var result = await mediator.Send(new DeleteAssetCommand(id));

    return FromResult(result);
  }
}
