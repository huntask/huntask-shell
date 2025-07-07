using MediatR;
using Microsoft.AspNetCore.Mvc;
using Huntask.Identity.Service.application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.LoginUser;
using Huntask.Identity.Service.Application.Commands.RegisterUserCommand;
using Huntask.Identity.Service.Application.Models;
using Huntask.Identity.Service.Presentation.Models;

namespace Huntask.Identity.Service.Presentation.Endpoints;

public static class IdentityEndpoints
{
  private static readonly string[] tags = ["User"];

  private const string Prefix = "api/identity";

  public static void RegisterIdentityEndpoints(this WebApplication app)
  {
    app.MapPost($"{Prefix}/register", RegisterAsync).WithTags(tags);
    app.MapPost($"{Prefix}/login", LoginAsync);
  }

  private static async Task<IResult> RegisterAsync(
    UserRegistrationModel model,
    IMediator mediator)
  {
    var result = await mediator.Send(new RegisterUserCommand(model));

    if (result.Success)
    {
      return Results.Created(
        "",
        new ApiResult<UserRegistrationModel>(result)
      );
    }

    return Results.BadRequest(
      new ApiResult<UserRegistrationModel>(result)
    );
  }

  private static async Task<IResult> LoginAsync(
    [FromBody] LoginModel model,
    IMediator mediator)
  {
    var result = await mediator.Send(new LoginUserCommand(model));

    if (!result.Success)
    {
      return Results.Unauthorized();
    }

    return Results.Ok(
      new ApiResult<LoginResponseModel>(result)
    );
  }
}