using System.Text.Json.Serialization;

namespace Huntask.Identity.Service.Application.Commands.RegisterUser;

public class UserRegistrationModel
{
  [FromForm(Name = "email")]
  [JsonPropertyName("email")]
  public string Email { get; set; } = string.Empty;

  [FromForm(Name = "password")]
  [JsonPropertyName("password")]
  public string Password { get; set; } = string.Empty;

  [FromForm(Name = "firstName")]
  [JsonPropertyName("firstName")]
  public string? FirstName { get; set; }

  [FromForm(Name = "lastName")]
  [JsonPropertyName("lastName")]
  public string? LastName { get; set; }

  [FromForm(Name = "avatar")]
  [JsonPropertyName("avatar")]
  public IFormFile? Avatar { get; set; }
};
