#region

using Infrastructure.Authentication.Options;
using Infrastructure.Authentication.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;

#endregion

namespace Infrastructure.Authentication;

public static class HostBuilderExtensions
{
    public static void AddAuthentication(this IHostApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<JwtOptionsSetup>();
        builder.Services.ConfigureOptions<JwtPostOptions>();
        builder.Services.AddSingleton<IJwtService, JwtService>();
        JsonWebTokenHandler.DefaultMapInboundClaims = true;
        JsonWebTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = UserClaims.Sub;
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
    }
}
