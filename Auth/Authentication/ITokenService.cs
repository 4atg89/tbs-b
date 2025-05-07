namespace Auth.Authentication;

public interface ITokenService
{
    public string GenerateAccessToken(Guid userId, string email, string nickname);
    public string GenerateRefreshToken(Guid jti, Guid userId, Guid securityStamp);
    public string GeneratePasswordChangeToken(Guid verificationId, DateTime expiresAt);
    public Guid? GetPasswordChangeVerificationId(string passwordToken);
    public DateTime GetRefreshTokenExpires();
    public UserRefreshModel? GetUserDataIfValid(string refreshToken);
}