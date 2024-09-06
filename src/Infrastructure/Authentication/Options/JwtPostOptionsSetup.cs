#region

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

#endregion

namespace Infrastructure.Authentication.Options;

public sealed class JwtPostOptions(IOptions<JwtOptions> jwtOptions) : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.Authority = _jwtOptions.Issuer;
        options.Audience = _jwtOptions.Audience;
        options.TokenValidationParameters = DefaultTokenValidationParametersBuilder.Build(_jwtOptions);
    }
}
