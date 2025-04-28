using Account.Dto;
using Account.Extensions;

namespace Account.Services;

public interface IUserVerificationService
{

    Task NotifyUser(Guid verificationId, string email, DateTime expiresAt);

    Task<ServiceResult<AuthenticatedUserResponse>> VerifyRegistration(Guid verificationId, string code);
    Task<ServiceResult<AuthenticatedUserResponse>> VerifyLogin(Guid verificationId, string code);
    Task<ServiceResult<PasswordResetResponse>> VerifyUserCanChangePassword(Guid verificationId, string code);
    Task<ServiceResult<AuthenticatedUserResponse>> DispatchTokenIfValid(string refreshToken);

}