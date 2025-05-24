using Auth.Dto;
using Auth.Extensions;
using JwtLibrary.Authentication;

namespace Auth.Services;

public interface IUserVerificationService
{
    Task NotifyUser(Guid verificationId, string email, DateTime expiresAt);
    Task<ServiceResult<AuthenticatedUserResponse>> VerifyUser(Guid verificationId, string code);
    Task<ServiceResult<PasswordResetResponse>> VerifyUserCanChangePassword(Guid verificationId, string code);
    Task<ServiceResult<AuthenticatedUserResponse>> RefreshToken(string refreshToken);
    UserRefreshModel? GetUserRefreshModel(string refreshToken);
    Task<bool> IsPasswordTokenValid(string refreshToken, string email);
}