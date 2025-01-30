using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace UPT.Infrastructure.Jwt;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;
    private readonly IDictionary<string, RefreshToken> _refreshTokens = new Dictionary<string, RefreshToken>();

    public void DeleteUser(int userId)
    {
        var refreshToken = _refreshTokens.FirstOrDefault(x => x.Value.UserId == userId);
        _refreshTokens.Remove(refreshToken);
    }

    public TokensModel GenerateTokens(int userId)
    {
        var accessToken = GenerateToken(userId);

        var refreshToken = GenerateRefreshToken(userId);
        _refreshTokens[refreshToken.Token] = refreshToken;

        return new TokensModel { AccessToken = accessToken, RefreshToken = refreshToken.Token };
    }

    public string RefreshAccessToken(string refreshToken)
    {
        if (!_refreshTokens.TryGetValue(refreshToken, out var storedToken) || storedToken.Expiration < DateTime.UtcNow)
        {
            throw new SecurityTokenException("Invalid or expired refresh token.");
        }

        // Generate a new access token
        return GenerateToken(storedToken.UserId);
    }

    private string GenerateToken(int userId)
    {
        Claim[] claims = { new("userId", userId.ToString()) };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiredHours));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken(int userId)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return new RefreshToken
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
            UserId = userId
        };
    }

    private class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
    }
}