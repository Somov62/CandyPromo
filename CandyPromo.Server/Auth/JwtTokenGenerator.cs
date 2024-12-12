using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CandyPromo.Server.Auth;

/// <summary>
/// Генератор токенов.
/// </summary>
public class JwtTokenGenerator(IOptions<JwtOptions> options)
{
    public string Generate(Guid userId, bool isAdmin)
    {
        Claim[] claims =
        [
            new ("userId", userId.ToString()),
            new (ClaimTypes.Role, isAdmin ? "Admin" : "User")
        ];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(options.Value.ExpiresHours),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
