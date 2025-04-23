namespace Account.Authentication;

public interface ITokenGenerator
{
    public string GenerateAccessToken(string email, string nickname, Guid userId);
}