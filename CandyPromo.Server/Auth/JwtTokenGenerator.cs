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
    /// <summary>
    /// Генерирует access токен.
    /// </summary>
    public string Generate(Guid userId, bool isAdmin, string userName = "")
    {
        // Информация, содержащаяся в токене.
        Claim[] claims =
        [
            new (ClaimTypes.NameIdentifier, userId.ToString()),
            new (ClaimTypes.Role, isAdmin ? "Admin" : "User")
        ];

        if (!string.IsNullOrEmpty(userName))
            claims?.Append(new Claim(ClaimTypes.Name, userName));

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
