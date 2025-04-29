namespace Account.Authentication;

//todo rename
public interface ITokenGenerator
{
    public string GenerateAccessToken(Guid userId, string email, string nickname);
    public string GenerateRefreshToken(Guid jti, Guid userId, Guid securityStamp);
    public string GeneratePasswordChangeToken(Guid verificationId);
    public Guid? GetPasswordChangeDataIfValid(string passwordToken);
    public DateTime GetRefreshTokenExpires();
    public UserRefreshModel? GetUserDataIfValid(string refreshToken);
}