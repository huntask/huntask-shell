using Huntask.Identity.Service.Application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.RegisterUser;
using Huntask.Common.Presentation.Controllers;

namespace Huntask.Identity.Service.Presentation.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity")]
public class IdentityApiController(IMediator mediator) : BaseApiController
{
  private readonly IMediator mediator = mediator;

  [HttpPost("register")]
  public async Task<IActionResult> RegisterAsync(UserRegistrationModel model)
  {
    var result = await mediator.Send(new RegisterUserCommand(model));
    return FromResult(result);
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
  {
    var result = await mediator.Send(new LoginUserCommand(model));
    return FromResult(result);
  }
}
