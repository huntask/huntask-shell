namespace Huntask.Identity.Service.Application.Commands.RegisterUser;

public record UserRegistrationModel(string Email, string Password, string FirstName, string LastName, string? AvatarAssetId);
