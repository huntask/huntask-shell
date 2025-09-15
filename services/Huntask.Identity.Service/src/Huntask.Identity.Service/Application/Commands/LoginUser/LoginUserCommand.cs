using Huntask.Common.Application.Models;
using Huntask.Identity.Service.Application.Models;

namespace Huntask.Identity.Service.Application.Commands.LoginUser;

public record LoginUserCommand(LoginModel Model) : IRequest<Result<LoginResponseModel>>;
