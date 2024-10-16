using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UPT.Infrastructure.Jwt;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    public string GenerateToken(int userId)
    {
        var jwtOptions = options.Value;

        Claim[] claims = [new("userId", userId.ToString())];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwtOptions.SecretKey")),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(jwtOptions.ExpiredHours));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
