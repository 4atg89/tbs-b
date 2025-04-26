using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtLibrary;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Account.Authentication;

public class TokenGenerator(IOptions<JwtOptions> options) : ITokenGenerator
{

    public string GenerateAccessToken(Guid userId, string email, string nickname)
    {
        //todo recheck that
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.UniqueName, nickname),
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.AccessSecretPrivate));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(options.Value.AccessJwtExpirationMinutes),
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(Guid userId)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, userId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.RefreshSecretPrivate));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(options.Value.RefreshJwtExpirationHours),
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Guid? GetIdIfValid(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            //todo redo RefreshSecretPrivate
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.RefreshSecretPrivate)),
            ValidIssuer = options.Value.Issuer,
            ValidAudience = options.Value.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var claims = principal.Claims;

            //todo maybe this is better?
            // var userId1 = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            //    ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Console.WriteLine($"bla bla {claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Sub)} {userId1}");

            var userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            _ = Guid.TryParse(userIdClaim, out var userId);
            return userId;

        }
        catch (Exception ex)
        {
            Console.WriteLine("Refresh" + ex.Message);
            Console.WriteLine(ex);
            return null;
        }
    }
}