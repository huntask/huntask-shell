using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Http;

namespace Huntask.Common.Presentation.Controllers;

public class BaseApiController : ControllerBase
{
  protected string HateoasBaseUrl => $"{Request.Scheme}://{Request.Host}/api/v{RouteData.Values["version"]}/";

  protected void SetAuthTokenCookie(string token, JwtOptions jwtOptions)
  {
    Response.Cookies.Append(Constants.Cookies.AuthToken, token, new CookieOptions
    {
      HttpOnly = true,
      Secure = true,
      SameSite = SameSiteMode.None,
      Expires = DateTimeOffset.UtcNow.AddHours(jwtOptions.ValidityHours)
    });
  }

  protected void RemoveAuthTokenCookie()
  {
    Response.Cookies.Delete(Constants.Cookies.AuthToken);
  }
}
