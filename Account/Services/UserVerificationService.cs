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
        await codeRepository.StoreEmailAndCode(verificationId, email, GenerateFourDigitCode(), expiresAt);
    }

    private async Task<ServiceResult<AuthenticatedUserResponse>> VerifyUser(Guid verificationId, string code)
    {
        var email = await codeRepository.FetchAndRemoveEmailAndCode(verificationId, code);
        if (email == null)
        {
            await codeRepository.ValidateAttemptsForFetchingEmailAndCode(verificationId, code);
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

    public Task<ServiceResult<PasswordResetResponse>> VerifyUserCanChangePassword(Guid verificationId, string code)
    {
        throw new NotImplementedException();
    }

    private static string GenerateFourDigitCode() =>
        new Random().Next(0, 10000).ToString("D4");

    public async Task<ServiceResult<AuthenticatedUserResponse>> VerifyRegistration(Guid verificationId, string code)
    {
        var email = await codeRepository.FetchAndRemoveEmailAndCode(verificationId, code);
        if (email == null)
        {
            await codeRepository.ValidateAttemptsForFetchingEmailAndCode(verificationId, code);
            return new(ClientErrorType.NotFound, "Code wasn't verified");
        }

        //todo if user is null (unexpected think what to do)
        var jti = Guid.NewGuid();
        var securityStamp = Guid.NewGuid();
        var user = await accountRepository.GetUserByEmail(email);
        var accessToken = tokenGenerator.GenerateAccessToken(user!.Id, user.Email, user.Nickname);


        var refreshToken = tokenGenerator.GenerateRefreshToken(jti, user.Id, securityStamp);
        return new(new AuthenticatedUserResponse { Token = accessToken, RefreshToken = refreshToken });
    }

    public async Task<ServiceResult<AuthenticatedUserResponse>> VerifyLogin(Guid verificationId, string code)
    {
        var email = await codeRepository.FetchAndRemoveEmailAndCode(verificationId, code);
        if (email == null)
        {
            await codeRepository.ValidateAttemptsForFetchingEmailAndCode(verificationId, code);
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
}