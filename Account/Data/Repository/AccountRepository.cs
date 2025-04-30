using Account.Data.Entity;
using Account.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Account.Data.Repository;

public class AccountRepository(IServiceProvider _serviceProvider) : IAccountRepository
{

    public async Task<T> ExecuteAsync<T>(Func<AccountDbContext, Task<T>> action)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
        return await action(context);
    }

    public async Task ExecuteAsync(Func<AccountDbContext, Task> action)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
        await action(context);
    }

    public async Task<UserEntity?> VerifyUser(string email, Guid jtiId, DateTime expiresAt) =>
        await ExecuteAsync(async context =>
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;
            context.UserSecurities.Add(UserSecurityCreate(user.Id, jtiId, expiresAt));
            user.IsVerified = true;
            await context.SaveChangesAsync();
            return user;
        });

    public async Task<UserEntity?> GetUserByEmail(string email) =>
        await ExecuteAsync(async context => await context.Users.SingleOrDefaultAsync(u => u.Email == email));


    public async Task<UserEntity?> GetUserById(Guid id) =>
        await ExecuteAsync(async context => await context.Users.SingleOrDefaultAsync(u => u.Id == id));

    public async Task Logout(Guid jtiId) => await ExecuteAsync(async context =>
        {
            var entity = await context.UserSecurities.FindAsync(jtiId);
            if (entity == null) return;
            context.UserSecurities.Remove(entity);
            await context.SaveChangesAsync();
        });

    public async Task<UserEntity> Register(UserEntity user) => await ExecuteAsync(async context =>
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
            var userUnverified = await context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
            if (userUnverified != null && !userUnverified.IsVerified) return userUnverified;

            throw UserAlreadyExists.From(mysqlEx);
        }

    });

    public async Task<UserEntity?> SecuredUserUpdate(Guid userId, Guid jtiId, Guid securityStamp, Guid newJtiId, DateTime expiresAt)
        => await ExecuteAsync(async context =>
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
             });

    private static UserSecurityEntity UserSecurityCreate(Guid userId, Guid jtiId, DateTime expiresAt) =>
        new() { JtiId = jtiId, UserId = userId, ExpiresAt = expiresAt };

    public async Task SetNewPassword(string email, string salt, string passwordHash) => await ExecuteAsync(async context =>
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null) return;
        user.Salt = salt;
        user.PasswordHash = passwordHash;
        user.SecurityStamp = Guid.NewGuid();
        await context.SaveChangesAsync();
    });
}