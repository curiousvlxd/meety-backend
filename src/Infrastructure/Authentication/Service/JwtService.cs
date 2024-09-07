using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities.User;
using Infrastructure.Authentication.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace Infrastructure.Authentication.Service;

public sealed class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
{   
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly TimeSpan _jwtTokenLifetime = TimeSpan.FromMinutes(15);
    private readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromDays(1);
    
    public async Task<string> GenerateJwtTokenAsync(UserId userId)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.Value.ToString()),
            new(JwtRegisteredClaimNames.Sub, userId.Value.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtTokenLifetime),
            SigningCredentials = credentials,
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.FromResult(tokenHandler.WriteToken(token));
    }

    public async Task<string> GenerateRefreshTokenAsync(UserId userId)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.Value.ToString()),
            new("token_type", "refresh")
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_refreshTokenLifetime),
            SigningCredentials = credentials,
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.FromResult(tokenHandler.WriteToken(token));
    }
}
