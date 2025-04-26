using Account.Data.Model;

namespace Account.Data.Repository;

public interface IAccountRepository
{

    Task<UserEntity> Register(UserEntity model);
    Task<UserEntity?> GetUserByEmail(string email);
    Task<UserEntity?> GetUserById(Guid id);

}