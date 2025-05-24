using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtLibrary;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtLibrary.Authentication;

public class TokenService(IOptions<JwtOptions> options) : ITokenService
{

    private const string SECURITY_STAMP = "SECURITY_STAMP";

    public string GenerateAccessToken(Guid userId, string email, string nickname)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.UniqueName, nickname),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
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

    public string GenerateRefreshToken(Guid jti, Guid userId, Guid securityStamp)
    {
        var claims = new List<Claim>()
        {
            new(SECURITY_STAMP, securityStamp.ToString()),
            new(JwtRegisteredClaimNames.Jti, jti.ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString())
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

    public UserRefreshModel? GetUserDataIfValid(string refreshToken)
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
            var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jtiClaim = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var securitStampClaim = principal.FindFirst(SECURITY_STAMP)?.Value;
            if (userIdClaim == null || jtiClaim == null || securitStampClaim == null) return null;
            return new UserRefreshModel { Id = new Guid(jtiClaim), UserId = new Guid(userIdClaim), SecurityStamp = new Guid(securitStampClaim) };
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DateTime GetRefreshTokenExpires()
    {
        //todo change to timeprovider
        return DateTime.UtcNow.AddHours(options.Value.RefreshJwtExpirationHours);
    }

    public string GeneratePasswordChangeToken(Guid verificationId, DateTime expiresAt)
    {

        var claims = new List<Claim>() { new(ClaimTypes.NameIdentifier, verificationId.ToString()), };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.RefreshSecretPrivate));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Guid? GetPasswordChangeVerificationId(string passwordToken)
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
            var principal = tokenHandler.ValidateToken(passwordToken, validationParameters, out SecurityToken validatedToken);
            var verificationId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (verificationId == null) return null;
            return new Guid(verificationId);
        }
        catch (Exception)
        {
            return null;
        }
    }
}