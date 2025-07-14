using Huntask.Identity.Service.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Huntask.Identity.Service.Application.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserRegistrationModel>>
{
  private const string BadRequestErrorMessage = "User creation has succeeded; User email: {Email}.";

  private readonly UserManager<IdentityUser> userManager;

  public RegisterUserCommandHandler(UserManager<IdentityUser> userManager)
  {
    ArgumentNullException.ThrowIfNull(userManager);

    this.userManager  = userManager;
  }

  public async Task<Result<UserRegistrationModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
  {
    var model = request.Model;
    var user = new IdentityUser(model.UserName)
    {
      Email = model.Email
    };
    var result = await userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
        Log.Logger.Debug(BadRequestErrorMessage, model.Email);
        return new Result<UserRegistrationModel>(true, HttpStatusCode.Created, model);
    }

    Log.Logger.Debug(BadRequestErrorMessage, model.Email);
    return new Result<UserRegistrationModel>(
        false,
        HttpStatusCode.BadRequest,
        model,
        [.. result.Errors.Select(e => e.Description)]
    );
  }
}
