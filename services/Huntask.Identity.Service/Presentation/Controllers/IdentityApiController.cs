using Microsoft.AspNetCore.Mvc;
using Huntask.Identity.Service.Application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.RegisterUserCommand;
using Huntask.Identity.Service.Application.Models;
using Huntask.Identity.Service.Presentation.Models;

namespace Huntask.Identity.Service.Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity")]
public class IdentityApiController(IMediator mediator) : ControllerBase
{
  private readonly IMediator mediator = mediator;

  [HttpPost("register")]
  public async Task<IActionResult> RegisterAsync(UserRegistrationModel model)
  {
    var result = await mediator.Send(new RegisterUserCommand(model));
    return result.Success
      ? StatusCode((int)result.StatusCode, new ApiResult<UserRegistrationModel>(result))
      : StatusCode((int)result.StatusCode, new ApiResult<UserRegistrationModel>(result));
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
  {
    var result = await mediator.Send(new LoginUserCommand(model));
    return result.Success
      ? StatusCode((int)result.StatusCode, new ApiResult<LoginResponseModel>(result))
      : StatusCode((int)result.StatusCode, new ApiResult<LoginResponseModel>(result));
  }
}
