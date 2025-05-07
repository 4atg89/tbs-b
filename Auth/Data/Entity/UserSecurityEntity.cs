namespace Auth.Data.Entity;

public class UserSecurityEntity
{
    public required Guid JtiId { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required Guid UserId { get; set; }
    public UserEntity? User { get; set; }
}