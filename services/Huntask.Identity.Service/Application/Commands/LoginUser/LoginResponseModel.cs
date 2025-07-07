using System.Text.Json.Serialization;

namespace Huntask.Identity.Service.Application.Models;

public class LoginResponseModel(string authToken, string userId, string userName)
{
  [JsonPropertyName("auth_token")]
  public string AuthToken { get; set; } = authToken;

  [JsonPropertyName("user_id")]
  public string UserId { get; set; } = userId;

  [JsonPropertyName("user_name")]
  public string UserName { get; set; } = userName;
}
