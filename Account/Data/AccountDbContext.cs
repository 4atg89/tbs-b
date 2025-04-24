using Account.Data.Model;
using Account.Model.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Account.Data;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().Configure();
        modelBuilder.Entity<RefreshTokenEntity>().Configure();

        base.OnModelCreating(modelBuilder);
    }
}