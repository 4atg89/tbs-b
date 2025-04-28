using Account.Data.Entity;

namespace Account.Data.Repository;

public interface IAccountRepository
{

    Task<UserEntity> Register(UserEntity model);
    Task<UserEntity?> GetUserByEmail(string email);
    Task<UserEntity?> SecuredUser(string email, Guid jtiId, DateTime expiresAt);
    Task<UserEntity?> GetUserById(Guid id);

}