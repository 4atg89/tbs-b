using Auth.Authentication;
using Auth.Data.Exceptions;
using Auth.Data.Repository;
using Auth.Dto;
using Auth.Dto.Extensions;
using Auth.Extensions;

namespace Auth.Services;

public class AuthService(
    IAuthRepository repository,
    IUserVerificationService userVerificationService,
    TimeProvider timeProvider,
    IEncryptor encryptor
    ) : IAuthService
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
            return new(new CodeExpirationResponse { ExpirationTime = expiresAt, VerificationId = registrationId });
        }
        catch (UserAlreadyExists ex)
        {
            return new(ClientErrorType.Conflict, ex.Message);
        }

    }

    public async Task<ServiceResult<CodeExpirationResponse>> Login(LoginRequest request)
    {
        var user = await repository.GetUserByEmail(request.Email);
        if (user == null || !user.IsVerified) return new(ClientErrorType.NotFound, "Email is not registered");
        if (encryptor.GetHash(request.Password, user.Salt) != user.PasswordHash) return new(ClientErrorType.Unauthorized, "Wrong password or Email!");
        var registrationId = Guid.NewGuid();
        var expiresAt = timeProvider.GetUtcNow().AddSeconds(300L).UtcDateTime;
        await userVerificationService.NotifyUser(registrationId, user.Email, expiresAt);
        return new(new CodeExpirationResponse { ExpirationTime = expiresAt, VerificationId = registrationId });
    }

    public async Task<ServiceResult<object>> Logout(string refreshToken)
    {
        var token = userVerificationService.GetUserRefreshModel(refreshToken);
        if (token == null) return new();
        await repository.Logout(token.Id);
        return new();
    }

    public async Task<ServiceResult<CodeExpirationResponse>> RestorePasswordByEmail(string email)
    {
        var user = await repository.GetUserByEmail(email);
        if (user == null || !user.IsVerified) return new(ClientErrorType.NotFound, "Email is not registered");
        var registrationId = Guid.NewGuid();
        var expiresAt = timeProvider.GetUtcNow().AddSeconds(300L).UtcDateTime;
        await userVerificationService.NotifyUser(registrationId, user.Email, expiresAt);
        return new(new CodeExpirationResponse { ExpirationTime = expiresAt, VerificationId = registrationId });
    }

    public async Task<ServiceResult<PasswordChangedResponse>> SetNewPassword(NewPasswordRequest request)
    {
        var isValid = await userVerificationService.IsPasswordTokenValid(request.ResetToken, request.Email);
        if (!isValid) return new(ClientErrorType.Unauthorized, "Token is not valid");
        var salt = encryptor.GetSalt();
        var passwordHash = encryptor.GetHash(request.Password, salt);
        await repository.SetNewPassword(request.Email, salt, passwordHash);
        return new(new PasswordChangedResponse { Message = "Password was successfully changed", Success = true });
    }

}