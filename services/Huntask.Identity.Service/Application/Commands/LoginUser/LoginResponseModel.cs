namespace Huntask.Identity.Service.Application.Models;

public class LoginResponseModel(string authToken, string userId)
{
  public string AuthToken { get; set; } = authToken;

  public string UserId { get; set; } = userId;
}
