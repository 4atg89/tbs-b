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
        //todo send email
        await codeRepository.StoreEmail(verificationId, email, GenerateFourDigitCode(), expiresAt);
    }

    public async Task<ServiceResult<AuthenticatedUserResponse>> VerifyUser(Guid verificationId, string code)
    {
        var email = await codeRepository.FetchAndRemoveEmail(verificationId, code);
        if (email == null)
        {
            await codeRepository.ValidateAttemptsForFetchingEmail(verificationId, code);
            return new(ClientErrorType.NotFound, "Code wasn't verified");
        }

        //todo if user is null (unexpected think what to do)
        var user = await accountRepository.GetUserByEmail(email);
        var accessToken = tokenGenerator.GenerateAccessToken(user!.Id, user.Email, user.Nickname);
        var jti = Guid.NewGuid();
        var securityStamp = Guid.NewGuid();
        var refreshToken = tokenGenerator.GenerateRefreshToken(jti, user.Id, securityStamp);
        return new(new AuthenticatedUserResponse { Token = accessToken, RefreshToken = refreshToken });
    }

    public async Task<ServiceResult<AuthenticatedUserResponse>> DispatchTokenIfValid(string refreshToken)
    {
        var refreshModel = tokenGenerator.GetUserDataIfValid(refreshToken);
        if (refreshModel == null) return new(ClientErrorType.NotFound, "Refresh token is not valid");
        var user = await accountRepository.GetUserById(refreshModel.UserId);
        var newAccessToken = tokenGenerator.GenerateAccessToken(user!.Id, user.Email, user.Nickname);
        var newRefreshToken = tokenGenerator.GenerateRefreshToken(Guid.NewGuid(), user!.Id, Guid.NewGuid());

        return new(new AuthenticatedUserResponse { Token = newAccessToken, RefreshToken = newRefreshToken });
    }

    private static string GenerateFourDigitCode() =>
        new Random().Next(0, 10000).ToString("D4");

}