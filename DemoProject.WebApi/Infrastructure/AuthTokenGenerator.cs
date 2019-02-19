using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DemoProject.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DemoProject.WebApi.Infrastructure
{
  public sealed class AuthTokenGenerator
  {
    private readonly AppSettings _appSettings;

    public AuthTokenGenerator(
      IOptions<AppSettings> options)
    {
      _appSettings = options.Value;
    }

    public string GenerateNewToken(Guid userId, string userRole)
    {
      Check.NotNullOrEmpty(userRole, nameof(userRole));

      var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, userId.ToString()),
          new Claim(ClaimTypes.Role, userRole)
        }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
