namespace Account.Data.Model;

public class User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Nickname { get; set; }
    public required string PasswordHash { get; set; }
    public required string Salt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsVerified { get; set; }
    public required ICollection<RefreshToken> RefreshTokens { get; set; }
}
