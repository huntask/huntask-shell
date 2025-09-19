using Huntask.Identity.Service.Application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.RegisterUser;
using Microsoft.Extensions.Options;
using Huntask.Identity.Service.Presentation.Models;
using Huntask.Common.Application.Extensions;

namespace Huntask.Identity.Service.Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity")]
public class IdentityApiController(
  IMediator mediator,
  IOptionsSnapshot<JwtOptions> jwtOptionsSnapshot,
  ILogger<IdentityApiController> logger) : BaseApiController
{
  private readonly JwtOptions jwtOptions = jwtOptionsSnapshot.Value;

  [HttpPost("register")]
  [Consumes("multipart/form-data")]
  public async Task<Result<UserRegistrationModel>> RegisterAsync([FromForm] UserRegistrationModel model)
  {
    return await mediator.Send(new RegisterUserCommand(model));
  }

  [HttpPost("login")]
  public async Task<Result<LoginResponseApiModel>> LoginAsync([FromBody] LoginModel model)
  {
    var result = await mediator.Send(new LoginUserCommand(model));

    if (result.IsFailed || string.IsNullOrWhiteSpace(result.Value?.AuthToken))
    {
      return Result.Fail(new Error(ErrorCodes.LoginFailed)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.LoginFailed))
        .LogErrors(logger, "Login failed: token is null or empty");
    }

    var token = result.Value.AuthToken;
    SetAuthTokenCookie(token, jwtOptions);
    return result.Map(value => new LoginResponseApiModel(result.Value.UserId));
  }


  [HttpPost("logout")]
  public IActionResult Logout()
  {
    RemoveAuthTokenCookie();
    return Ok();
  }
}
