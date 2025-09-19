namespace Huntask.Identity.Service.Application.Commands.LoginUser;

public record LoginUserCommand(LoginModel Model) : IRequest<Result<LoginResponseModel>>;
