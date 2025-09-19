using Huntask.Identity.Service.Domain.Models;
using Huntask.Identity.Service.Infrastructure.Contexts;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Huntask.Identity.Service.Application.Commands.RegisterUser;

public class RegisterUserCommandHandler(
  IdentityContext dbContext,
  UserManager<User> userManager,
  IEndpointNameFormatter endpointNameFormatter,
  ISendEndpointProvider sendEndpointProvider) : IRequestHandler<RegisterUserCommand, Result<UserRegistrationModel>>
{
  public async Task<Result<UserRegistrationModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
  {
    var model = request.Model;
    var user = new User
    {
      UserName = model.Email ?? model.FirstName,
      Email = model.Email,
      FirstName = model.FirstName ?? string.Empty,
      LastName = model.LastName ?? string.Empty,
    };

    var result = await userManager.CreateAsync(user, model.Password);

    if (result.Succeeded && model.Avatar is not null)
    {
      await using var avatarStream = new MemoryStream();
      await model.Avatar.CopyToAsync(avatarStream, cancellationToken);
      avatarStream.Position = 0;

      var endpointName = endpointNameFormatter.Message<UploadAvatarCommand>();
      var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{endpointName}"));

      await endpoint.Send<UploadAvatarCommand>(new
      {
        UserId = user.Id,
        Avatar = avatarStream,
        ContainerName = "avatars",
        model.Avatar.FileName,
        model.Avatar.ContentType,
      }, cancellationToken);

      await dbContext.SaveChangesAsync(cancellationToken);
    }

    if (result.Succeeded)
    {
      return Result.Ok(model)
        .WithSuccess(new Success("User registered successfully")
          .WithMetadata(ResultMetadataKeys.SuccessStatusCode, HttpStatusCode.Created));
    }

    return Result.Fail(new Error(ErrorCodes.RegistrationIsNotSuccessful)
      .WithMetadata(ResultMetadataKeys.ErrorCode, ErrorCodes.RegistrationIsNotSuccessful));
  }
}
