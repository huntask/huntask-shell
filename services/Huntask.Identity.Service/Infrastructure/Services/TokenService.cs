using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Huntask.Identity.Service.Infrastructure.Models.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Huntask.Identity.Service.Infrastructure.Services;

public class TokenService(IOptionsSnapshot<JwtOptions> options) : ITokenService
{
  private readonly JwtOptions jwtOptions = options.Value;

  public string GenerateToken(IdentityUser user)
  {
    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id),
      new Claim(ClaimTypes.Email, user.Email ?? ""),
      new Claim(ClaimTypes.NameIdentifier, user.UserName ?? "")
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: jwtOptions.Issuer,
      audience: jwtOptions.Audience,
      claims: claims,
      expires: DateTime.UtcNow.AddHours(jwtOptions.ValidityHours),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}