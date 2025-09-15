using Huntask.Identity.Service.Application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.RegisterUser;
using Huntask.Common.Presentation.Controllers;
using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.Extensions.Options;
using Huntask.Common.Presentation;
using Huntask.Identity.Service.Presentation.Models;
using Huntask.Common.Application.Models;

namespace Huntask.Identity.Service.Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity")]
public class IdentityApiController(IMediator mediator, IOptionsSnapshot<JwtOptions> jwtOptionsSnapshot) : BaseApiController
{
  private readonly JwtOptions jwtOptions = jwtOptionsSnapshot.Value;

  [HttpPost("register")]
  [Consumes("multipart/form-data")]
  public async Task<IActionResult> RegisterAsync([FromForm] UserRegistrationModel model)
  {
    // TODO[identity]: Implement model validation with FluentValidation
    var result = await mediator.Send(new RegisterUserCommand(model));
    return FromResult(result);
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
  {
    // TODO[identity]: Implement model validation with FluentValidation
    var result = await mediator.Send(new LoginUserCommand(model));
    var token = result.Data?.AuthToken;

    if (!result.Success || token is null)
    {
      return FromResult(result);
    }

    SetAuthTokenCookie(token);

    var apiResult = new Result<LoginResponseApiModel>(
      result.Success,
      result.StatusCode,
      new LoginResponseApiModel(result.Data?.UserId),
      result.Errors
    );
    return FromResult(apiResult);
  }


  [HttpPost("logout")]
  public IActionResult Logout()
  {
    RemoveAuthTokenCookie();
    return Ok();
  }

  // TODO[identity]: Move to a dedicated service or middleware
  private void SetAuthTokenCookie(string token)
  {
    Response.Cookies.Append(Constants.Cookies.AuthToken, token, new CookieOptions
    {
      HttpOnly = true,
      Secure = true,
      SameSite = SameSiteMode.None,
      Expires = DateTimeOffset.UtcNow.AddHours(jwtOptions.ValidityHours)
    });
  }

  // TODO[identity]: Move to a dedicated service or middleware
  private void RemoveAuthTokenCookie()
  {
    Response.Cookies.Delete(Constants.Cookies.AuthToken);
  }
}
