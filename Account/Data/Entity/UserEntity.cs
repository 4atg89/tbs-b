namespace Account.Data.Entity;

public class UserEntity
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Nickname { get; set; }
    public required string PasswordHash { get; set; }
    public required string Salt { get; set; }
    public required Guid SecurityStamp { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsVerified { get; set; }
    public ICollection<UserSecurityEntity>? UserSecurities { get; set; }
}
