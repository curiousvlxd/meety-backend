using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Authentication.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace Infrastructure.Authentication.Service;

public sealed class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
{   
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly TimeSpan _jwtTokenLifetime = TimeSpan.FromMinutes(15);
    private readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromDays(1);
    
    public async Task<string> GenerateJwtTokenAsync(string messengerUserId, bool isRefreshToken = false)
    {
        var claims = new List<Claim>
        {
            new(UserClaims.Sub, messengerUserId)
        };

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(isRefreshToken ? _refreshTokenLifetime : _jwtTokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.FromResult(tokenHandler.WriteToken(token));
    }
}
