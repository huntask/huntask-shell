using System.Text.Json.Serialization;

namespace Huntask.Identity.Service.Application.Models;

public class LoginResponseModel(string authToken, string userId, string email)
{
  [JsonPropertyName("authToken")]
  public string AuthToken { get; set; } = authToken;

  [JsonPropertyName("userId")]
  public string UserId { get; set; } = userId;

  [JsonPropertyName("email")]
  public string Email { get; set; } = email;
}
