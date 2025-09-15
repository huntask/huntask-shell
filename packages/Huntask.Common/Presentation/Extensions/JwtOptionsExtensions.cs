using System.Text;
using Huntask.Common.Infrastructure.Models.Options;
using Microsoft.IdentityModel.Tokens;

namespace Huntask.Common.Presentation.Extensions;

public static class JwtOptionsExtensions
{
  public static TokenValidationParameters GetTokenValidationParameters(this JwtOptions jwtOptions)
  {
    return new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidIssuer = jwtOptions.Issuer,
      ValidateAudience = true,
      ValidAudience = jwtOptions.Audience,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
    };
  }
}