using Account.Dto;
using Account.Extensions;

namespace Account.Services;

public interface IUserVerificationService
{

    Task NotifyUser(Guid verificationId, string email, DateTime expiresAt);

    Task<ServiceResult<AuthenticatedUserResponse>> VerifyUser(Guid verificationId, string code);

}