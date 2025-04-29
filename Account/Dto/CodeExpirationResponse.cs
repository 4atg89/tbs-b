namespace Account.Dto;

public class CodeExpirationResponse
{

    public Guid VerificationId { get; set; }
    public DateTime ExpirationTime { get; set; }
}