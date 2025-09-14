namespace Huntask.Common.Infrastructure.Models.Options;

public class JwtOptions
{
  public string Issuer { get; set; } = "";
  public string Audience { get; set; } = "";
  public string Secret { get; set; } = "";
  public int ValidityHours { get; set; } = 1;
};
