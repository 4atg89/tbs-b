using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace JwtLibrary;

public static class JwtExtensions
{

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new JwtOptions();
        var section = configuration.GetSection("Jwt");
        section.Bind(options);
        services.Configure<JwtOptions>(section);

        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                // todo check if it is better then other solution in token generator
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //todo redo AccessSecretPrivate
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.AccessSecretPrivate)),
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }

}
