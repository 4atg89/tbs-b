using Account.Data.Exceptions;
using Account.Data.Model;
using Account.Data.Repository;
using Account.Dto;
using Account.Dto.Extensions;
using Account.Extensions;

namespace Account.Services;

public class AccountService(
    IAccountRepository repository,
    IUserVerificationService userVerificationService,
    TimeProvider timeProvider,
    IEncryptor encryptor
    ) : IAccountService
{
    public async Task<ServiceResult<CodeExpirationResponse>> Register(RegistrationRequest request)
    {
        var createdAt = timeProvider.GetUtcNow();
        var registrationId = Guid.NewGuid();
        var user = request.MapRegistrationRequestToUserEntity(encryptor, createdAt.UtcDateTime);
        try
        {
            //todo do smth with verification
            var result = await repository.Register(user);
            var expiresAt = createdAt.AddSeconds(300L).UtcDateTime;
            //todo should remove await and start new task inside?
            await userVerificationService.NotifyUser(registrationId, user.Email, expiresAt);
            return new(new CodeExpirationResponse { ExpirationTime = expiresAt, Id = registrationId });
        }
        catch (UserAlreadyExists ex)
        {
            return new(ClientErrorType.Conflict, ex.Message);
        }

    }

}