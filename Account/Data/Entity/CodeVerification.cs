namespace Account.Data.Model;

public class CodeVerification
{
    public Guid Id { get; set; }
    // token for login or sigh up or restore and type of login(email sms or smth) and don't forgot to remove email
    // also login attemps try to do with redis
    public required string Email { get; set; }
    public required string Code { get; set; }
    public DateTime Expires { get; set; }
    public Guid UserId { get; set; }
}