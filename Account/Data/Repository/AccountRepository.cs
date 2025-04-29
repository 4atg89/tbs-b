using Account.Data.Entity;
using Account.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Account.Data.Repository;

public class AccountRepository(AccountDbContext context) : IAccountRepository
{
    public async Task<UserEntity?> AuthenticateUser(string email, Guid jtiId, DateTime expiresAt)
    {
        var user = await GetUserByEmail(email);
        if (user == null) return null;
        context.UserSecurities.Add(UserSecurityCreate(user.Id, jtiId, expiresAt));
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity?> GetUserByEmail(string email) =>
        await context.Users.SingleOrDefaultAsync(u => u.Email == email);


    public async Task<UserEntity?> GetUserById(Guid id) =>
        await context.Users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task Logout(Guid jtiId)
    {
        var entity = await context.UserSecurities.FindAsync(jtiId);
        if (entity == null) return;
        context.UserSecurities.Remove(entity);
        await context.SaveChangesAsync();
    }

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
            //todo finish with unverified users
            throw UserAlreadyExists.From(mysqlEx);
        }

    }

    public async Task<UserEntity?> SecuredUserUpdate(Guid userId, Guid jtiId, Guid securityStamp, Guid newJtiId, DateTime expiresAt)
    {
        var user = await context.Users
            .Include(u => u.UserSecurities)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null || user.SecurityStamp != securityStamp) return null;

        var securityMatch = user.UserSecurities?.FirstOrDefault(s => s.JtiId == jtiId);
        if (securityMatch == null) return null;

        context.UserSecurities.Remove(securityMatch);
        context.UserSecurities.Add(UserSecurityCreate(userId, newJtiId, expiresAt));

        await context.SaveChangesAsync();

        return user;
    }

    private static UserSecurityEntity UserSecurityCreate(Guid userId, Guid jtiId, DateTime expiresAt) =>
        new() { JtiId = jtiId, UserId = userId, ExpiresAt = expiresAt };
}