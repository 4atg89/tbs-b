using Auth.Data.Entity;

namespace Auth.Dto.Extensions;

public static class DtoMappingExtensions
{
    public static UserEntity MapRegistrationRequestToUserEntity(this RegistrationRequest it, IEncryptor encryptor, DateTime createdAt)
    {
        var salt = encryptor.GetSalt();
        return new UserEntity
        {
            Email = it.Email,
            Nickname = it.Nickname,
            PasswordHash = encryptor.GetHash(it.Password, salt),
            Salt = salt,
            SecurityStamp = Guid.NewGuid(),
            CreatedAt = createdAt
        };
    }
}