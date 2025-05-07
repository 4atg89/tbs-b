using Auth.Dto;
using Auth.Extensions;

namespace Auth.Services;

public interface IAuthService
{

    Task<ServiceResult<CodeExpirationResponse>> Register(RegistrationRequest request);
    Task<ServiceResult<CodeExpirationResponse>> Login(LoginRequest request);
    Task<ServiceResult<object>> Logout(string refreshToken);
    Task<ServiceResult<CodeExpirationResponse>> RestorePasswordByEmail(string email);
    Task<ServiceResult<PasswordChangedResponse>> SetNewPassword(NewPasswordRequest request);
}