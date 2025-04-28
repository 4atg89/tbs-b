namespace Account.Data.Entity;

public class UserSecurityEntity
{
    public required Guid JtiId { get; set; }
    public required Guid SecurityStamp { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required Guid UserId { get; set; }
    public required UserEntity User { get; set; }
}