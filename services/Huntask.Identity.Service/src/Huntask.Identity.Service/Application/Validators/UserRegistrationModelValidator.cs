using FluentValidation;
using Huntask.Identity.Service.Application.Commands.RegisterUser;

namespace Huntask.Identity.Service.Application.Validators;

public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
{
  public UserRegistrationModelValidator()
  {
    RuleFor(p => p.Email)
      .NotEmpty().WithMessage("Email should not be empty")
      .EmailAddress().WithMessage("Email should have correct format");

    RuleFor(p => p.Password)
      .NotEmpty().WithMessage("Password should not be empty")
      .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
  }
}