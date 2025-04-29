using Account.Authentication;
using Account.Data.Exceptions;
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

    public async Task<ServiceResult<CodeExpirationResponse>> Login(LoginRequest request)
    {
        var user = await repository.GetUserByEmail(request.Email);
        if (user == null) return new(ClientErrorType.NotFound, "Email is not registered");
        if (encryptor.GetHash(request.Password, user.Salt) != user.PasswordHash) return new(ClientErrorType.Unauthorized, "Wrong password or Email!");
        var registrationId = Guid.NewGuid();
        var expiresAt = timeProvider.GetUtcNow().AddSeconds(300L).UtcDateTime;
        await userVerificationService.NotifyUser(registrationId, user.Email, expiresAt);
        return new(new CodeExpirationResponse { ExpirationTime = expiresAt, Id = registrationId });
    }

    public async Task<ServiceResult<object>> Logout(string refreshToken)
    {
        var token = userVerificationService.GetUserRefreshModel(refreshToken);
        if (token == null) return new();
        await repository.Logout(token.Id);
        return new();
    }

    public async Task<ServiceResult<CodeExpirationResponse>> RestorePassword(ResetPasswordRequest request)
    {
        var user = await repository.GetUserByEmail(request.Email);
        if (user == null) return new(ClientErrorType.NotFound, "Email is not registered");
        var registrationId = Guid.NewGuid();
        var expiresAt = timeProvider.GetUtcNow().AddSeconds(300L).UtcDateTime;
        await userVerificationService.NotifyUser(registrationId, user.Email, expiresAt);
        return new(new CodeExpirationResponse { ExpirationTime = expiresAt, Id = registrationId });
    }

    public Task<ServiceResult<CodeExpirationResponse>> SetNewPassword(NewPasswordRequest request)
    {
        throw new NotImplementedException();
    }
}