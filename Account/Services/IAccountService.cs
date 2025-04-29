using Account.Dto;
using Account.Extensions;

namespace Account.Services;

public interface IAccountService
{

    Task<ServiceResult<CodeExpirationResponse>> Register(RegistrationRequest request);
    Task<ServiceResult<CodeExpirationResponse>> Login(LoginRequest request);
    Task<ServiceResult<object>> Logout(string refreshToken);
    Task<ServiceResult<CodeExpirationResponse>> RestorePassword(ResetPasswordRequest request);
    Task<ServiceResult<CodeExpirationResponse>> SetNewPassword(NewPasswordRequest request);
}