using Account.Data.Exceptions;
using Account.Data.Model;
using Account.Data.Repository;
using Account.Dto;
using Account.Extensions;

namespace Account.Services;

public class AccountService(
    IAccountRepository repository,
    TimeProvider timeProvider,
    IEncryptor encryptor
    ) : IAccountService
{
    public async Task<ServiceResult<CodeExpirationDto>> Register(RegistrationRequest request)
    {
        var salt = encryptor.GetSalt();
        var createdAt = timeProvider.GetUtcNow();
        var user = new UserEntity
        {
            Email = request.Email,
            Nickname = request.Nickname,
            PasswordHash = encryptor.GetHash(request.Password, salt),
            Salt = salt,
            CreatedAt = createdAt.UtcDateTime
        };

        try
        {
            var result = await repository.Register(user);
            return new(new CodeExpirationDto { ExpirationTime = createdAt.AddSeconds(300L).UtcDateTime, Id = result.Id });
        }
        catch (UserAlreadyExists ex)
        {
            return new(ClientErrorType.Conflict, ex.Message);
        }

    }

}