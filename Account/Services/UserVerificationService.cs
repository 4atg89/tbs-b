using Account.Authentication;
using Account.Data.Repository;
using Account.Dto;
using Account.Extensions;

namespace Account.Services;

public class UserVerificationService(
    ICodeRepository codeRepository,
    IAccountRepository accountRepository,
    ITokenGenerator tokenGenerator
) : IUserVerificationService
{

    public async Task NotifyUser(Guid verificationId, string email, DateTime expiresAt)
    {
        var code = GenerateFourDigitCode();
        //todo send email
        await codeRepository.StoreEmailAndCode(verificationId, email, code, expiresAt);
    }

    public async Task<ServiceResult<AuthenticatedUserResponse>> RefreshToken(string refreshToken)
    {
        var rm = tokenGenerator.GetUserDataIfValid(refreshToken);
        if (rm == null) return new(ClientErrorType.NotFound, "Refresh token is not valid");
        var jti = Guid.NewGuid();
        var user = await accountRepository.SecuredUserUpdate(rm.UserId, rm.Id, rm.SecurityStamp, jti, tokenGenerator.GetRefreshTokenExpires());
        if (user == null) return new(ClientErrorType.NotFound, "Refresh token is not valid");
        var newAccessToken = tokenGenerator.GenerateAccessToken(user!.Id, user.Email, user.Nickname);
        var newRefreshToken = tokenGenerator.GenerateRefreshToken(jti, user.Id, user.SecurityStamp);

        return new(new AuthenticatedUserResponse { Token = newAccessToken, RefreshToken = newRefreshToken });
    }

    public Task<ServiceResult<PasswordResetResponse>> VerifyUserCanChangePassword(Guid verificationId, string code)
    {
        throw new NotImplementedException();
    }

    private static string GenerateFourDigitCode() =>
        new Random().Next(0, 10000).ToString("D4");

    public async Task<ServiceResult<AuthenticatedUserResponse>> VerifyUser(Guid verificationId, string code)
    {
        var email = await codeRepository.FetchAndRemoveEmailAndCode(verificationId, code);
        if (email == null)
        {
            await codeRepository.ValidateAttemptsForFetchingEmailAndCode(verificationId, code);
            return new(ClientErrorType.NotFound, "Code wasn't verified");
        }

        var jti = Guid.NewGuid();
        var user = await accountRepository.AuthenticateUser(email, jti, tokenGenerator.GetRefreshTokenExpires());
        //todo if user is null (unexpected think what to do)
        var accessToken = tokenGenerator.GenerateAccessToken(user!.Id, user.Email, user.Nickname);
        var refreshToken = tokenGenerator.GenerateRefreshToken(jti, user.Id, user.SecurityStamp);
        return new(new AuthenticatedUserResponse { Token = accessToken, RefreshToken = refreshToken });
    }

    public UserRefreshModel? GetUserRefreshModel(string refreshToken)
    {
        return tokenGenerator.GetUserDataIfValid(refreshToken);
    }
}