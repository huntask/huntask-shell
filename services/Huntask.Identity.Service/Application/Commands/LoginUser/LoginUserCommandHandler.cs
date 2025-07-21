using Huntask.Identity.Service.Application.Models;
using Microsoft.AspNetCore.Identity;
using Huntask.Identity.Service.Infrastructure.Services;
using Huntask.Common.Application.Models;
using Huntask.Identity.Service.Domain.Models;

namespace Huntask.Identity.Service.Application.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginResponseModel>>
{
  private const string UnauthorizedErrorMessage = "Login or password is incorrect.";

  private readonly UserManager<User> userManager;
  private readonly SignInManager<User> signInManager;
  private readonly ITokenService tokenService;

  public LoginUserCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenService tokenService)
  {
    ArgumentNullException.ThrowIfNull(userManager);
    ArgumentNullException.ThrowIfNull(signInManager);
    ArgumentNullException.ThrowIfNull(tokenService);

    this.userManager = userManager;
    this.signInManager = signInManager;
    this.tokenService = tokenService;
  }

  public async Task<Result<LoginResponseModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
  {
    var model = request.Model;
    var user = await userManager.FindByEmailAsync(model.Email);
    if (user == null)
    {
      Log.Logger.Debug("User login has not succeeded; The login model is empty.");
      return new Result<LoginResponseModel>(
        false,
        HttpStatusCode.Unauthorized,
        null,
        [UnauthorizedErrorMessage]
      );
    }

    var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
    if (!passwordCheck.Succeeded)
    {
      Log.Logger.Debug("User login has not succeeded; The login model is empty.");
      return new Result<LoginResponseModel>(
        false,
        HttpStatusCode.Unauthorized,
        null,
        [UnauthorizedErrorMessage]
      );
    }

    var token = tokenService.GenerateToken(user);
    Log.Logger.Debug("User login has succeeded; Email: {Email}", model.Email);

    return new Result<LoginResponseModel>(
      true,
      HttpStatusCode.OK,
      new LoginResponseModel(token, user.Id, user.Email ?? "")
    );
  }
}
