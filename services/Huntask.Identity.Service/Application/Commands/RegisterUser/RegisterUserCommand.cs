using MediatR;
using Huntask.Identity.Service.Application.Models;

namespace Huntask.Identity.Service.Application.Commands.RegisterUserCommand;

public record RegisterUserCommand(UserRegistrationModel Model) : IRequest<Result<UserRegistrationModel>>;
