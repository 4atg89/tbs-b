namespace JwtLibrary;

public class JwtOptions
{
    public string AccessSecret { get; set; } = string.Empty;
    public double AccessJwtExpirationMinutes { get; set; }
    public string RefreshSecret { get; set; } = string.Empty;
    public double RefreshJwtExpirationHours { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
