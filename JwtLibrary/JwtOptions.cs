namespace JwtLibrary;

public class JwtOptions
{
    public string AccessSecretPrivate { get; set; } = string.Empty;
    public string AccessSecretPublic { get; set; } = string.Empty;
    public double AccessJwtExpirationMinutes { get; set; }
    public string RefreshSecretPrivate { get; set; } = string.Empty;
    public string RefreshSecretPublic { get; set; } = string.Empty;
    public double RefreshJwtExpirationHours { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
