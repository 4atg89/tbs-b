using Account.Dto;
using Account.Extensions;

namespace Account.Services;

public interface IAccountService
{

    Task<ServiceResult<CodeExpirationResponse>> Register(RegistrationRequest request);
    Task<ServiceResult<CodeExpirationResponse>> Login(LoginRequest request);
    Task<ServiceResult<CodeExpirationResponse>> RestorePassword(PasswordRestoringRequest request);
}