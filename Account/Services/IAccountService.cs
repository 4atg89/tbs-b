using Account.Dto;
using Account.Extensions;

namespace Account.Services;

public interface IAccountService
{
    
    Task<ServiceResult<CodeExpirationDto>> Register(RegistrationRequest request);
}