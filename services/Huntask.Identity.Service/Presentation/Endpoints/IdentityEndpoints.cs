using Huntask.Identity.Service.Infrastructure.Services;
using Huntask.Identity.Service.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
    UserManager<IdentityUser> userManager,
    HttpContext http)
  {
    var user = new IdentityUser(model.UserName)
    {
      Email = model.Email
    };
    var result = await userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
      Log.Logger.Debug("User creationg has succeeded; User email: {Email}.", model.Email);
      return Results.Created("", new ApiResponse<object>(true));
    }

    Log.Logger.Debug("User creationg has not succeeded; User email: {Email}.", model.Email);
    return Results.BadRequest(new ApiResponse<UserRegistrationModel>(
      false,
      model,
      [.. result.Errors.Select(e => e.Description)]
    ));
  }

  private static async Task<IResult> LoginAsync(
    [FromBody] LoginModel model,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    ITokenService tokenService)
  {
    var user = await userManager.FindByNameAsync(model.UserName);
    if (user == null)
    {
      Log.Logger.Debug("User login has not succeeded; The login model is empty.");
      return Results.Unauthorized();
    }

    var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
    if (!passwordCheck.Succeeded)
    {
      Log.Logger.Debug("User login has not succeeded; The login model is empty.");
      return Results.Unauthorized();
    }

    var token = tokenService.GenerateToken(user);
    Log.Logger.Debug("User login has succeeded; Username: {Username}", model.UserName);

    return Results.Ok(new ApiResponse<LoginResponseModel>(
      true,
      new LoginResponseModel(token, user.Id, user.UserName ?? "")
    ));
  }
}