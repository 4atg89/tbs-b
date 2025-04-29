using Account.Data.Entity;

namespace Account.Data.Repository;

public interface IAccountRepository
{

    Task<UserEntity> Register(UserEntity model);
    Task<UserEntity?> GetUserByEmail(string email);
    Task<UserEntity?> GetUserById(Guid id);
    Task<UserEntity?> SecuredUserUpdate(Guid userId, Guid jtiId, Guid securityStamp, Guid newJtiId, DateTime expiresAt);
    Task<UserEntity?> VerifyUser(string email, Guid jtiId, DateTime expiresAt);
    Task SetNewPassword(string email, string salt, string passwordHash);
    Task Logout(Guid jtiId);

}