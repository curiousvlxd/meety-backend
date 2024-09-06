#region

using Infrastructure.Authentication.Options;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace Infrastructure.Authentication;

public static class DefaultTokenValidationParametersBuilder
{
    public static TokenValidationParameters Build(JwtOptions options)
    {
        return new TokenValidationParameters
        {
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience
        };
    }
}
