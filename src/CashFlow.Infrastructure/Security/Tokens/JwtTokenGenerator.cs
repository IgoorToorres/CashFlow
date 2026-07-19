using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Infrastructure.Security.Tokens;

public class JwtTokenGenerator(
    uint expirationTimeMinutes,
    string signinKey
) : IAccessTokenGenerator
{

    private readonly uint _expirationTimeMinutes = expirationTimeMinutes;
    private readonly string _signinKey = signinKey;

    public string Generator(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Sid, user.UserIdentifier.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims),
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signinKey);
        return new SymmetricSecurityKey(key);
    }
}