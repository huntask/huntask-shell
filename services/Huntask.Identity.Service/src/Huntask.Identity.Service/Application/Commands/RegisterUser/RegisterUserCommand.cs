using Huntask.Common.Application.Models;

namespace Huntask.Identity.Service.Application.Commands.RegisterUser;

public record RegisterUserCommand(UserRegistrationModel Model) : IRequest<Result<UserRegistrationModel>>;
