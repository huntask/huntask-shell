using Microsoft.AspNetCore.Identity;
using Huntask.Identity.Service.Infrastructure.Services;
using Huntask.Identity.Service.Domain.Models;
using Huntask.Identity.Service.Application.Validators;
using Huntask.Common.Application.Extensions;

namespace Huntask.Identity.Service.Application.Commands.LoginUser;

public class LoginUserCommandHandler(
  UserManager<User> userManager,
  SignInManager<User> signInManager,
  ITokenService tokenService) : IRequestHandler<LoginUserCommand, Result<LoginResponseModel>>
{
  public async Task<Result<LoginResponseModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
  {
    var model = request.Model;

    var validator = new LoginModelValidator();
    var validationResult = await validator.ValidateAsync(model, cancellationToken);
    if (!validationResult.IsValid)
    {
      return Result.Fail(new Error(ErrorCodes.ValidationError)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.ValidationError))
        .AddValidationErrors(validationResult);
    }

    var user = await userManager.FindByEmailAsync(model.Email);
    if (user == null)
    {
      return Result.Fail(new Error(ErrorCodes.Unauthorized)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.Unauthorized));
    }

    var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
    if (!passwordCheck.Succeeded)
    {
      return Result.Fail(new Error(ErrorCodes.PasswordIsNotCorrect)
        .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.PasswordIsNotCorrect));
    }

    var token = tokenService.GenerateToken(user);
    return Result.Ok(new LoginResponseModel(token, user.Id));
  }
}