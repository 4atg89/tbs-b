using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
              x.RequireHttpsMetadata = false;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.AccessSecret)),
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
