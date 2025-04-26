using Account.Data.Exceptions;
using Account.Data.Model;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Account.Data.Repository;

public class AccountRepository(AccountDbContext context) : IAccountRepository
{
    public async Task<UserEntity?> GetUserByEmail(string email) =>
        await context.Users.SingleOrDefaultAsync(u => u.Email == email);


    public async Task<UserEntity?> GetUserById(Guid id) =>
        await context.Users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task<UserEntity> Register(UserEntity user)
    {
        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
        {
            throw UserAlreadyExists.From(mysqlEx);
        }

    }
}