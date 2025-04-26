namespace Account.Authentication;

//todo rename
public interface ITokenGenerator
{
    public string GenerateAccessToken(Guid userId, string email, string nickname);
    public string GenerateRefreshToken(Guid userId);
    public Guid? GetIdIfValid(string token);
}