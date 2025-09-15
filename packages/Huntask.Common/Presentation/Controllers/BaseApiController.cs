using Huntask.Common.Presentation.Models;
using Huntask.Common.Application.Models;

namespace Huntask.Common.Presentation.Controllers;

public class BaseApiController : ControllerBase
{
  public string HateoasBaseUrl => $"{Request.Scheme}://{Request.Host}/api/v{RouteData.Values["version"]}/";

  protected IActionResult FromResult<T>(Result<T> result)
  {
    return StatusCode((int)result.StatusCode, new ApiResult<T>(result));
  }
}
