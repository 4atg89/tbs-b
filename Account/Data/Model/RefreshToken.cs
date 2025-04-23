namespace Account.Data.Model;

public class RefreshToken
{
    public Guid Id { get; set; }
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}