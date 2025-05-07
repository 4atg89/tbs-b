namespace Auth.Authentication;

public class UserRefreshModel()
{
    public required Guid Id { get; set; }
    public required Guid SecurityStamp { get; set; }
    public required Guid UserId { get; set; }
}