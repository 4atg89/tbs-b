namespace Account.Authentication;

public interface ITokenGenerator
{
    public string GenerateAccessToken(Guid userId, string email, string nickname);
}