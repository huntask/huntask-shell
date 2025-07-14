using Microsoft.AspNetCore.Mvc;
using Huntask.Assets.Service.Presentation.Models;
using Huntask.Assets.Service.Application.Models;

namespace Huntask.Assets.Service.Presentation.Controllers;

public class BaseApiController : ControllerBase
{
  public string HateoasBaseUrl => $"{Request.Scheme}://{Request.Host}/api/v{RouteData.Values["version"]}/";

  protected IActionResult FromResult<T>(Result<T> result)
  {
    return StatusCode((int)result.StatusCode, new ApiResult<T>(result));
  }
}
