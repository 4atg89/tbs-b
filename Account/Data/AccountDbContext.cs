using Account.Data.Model;
using Account.Model.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Account.Data;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Configure();
        modelBuilder.Entity<RefreshToken>().Configure();

        base.OnModelCreating(modelBuilder);
    }
}