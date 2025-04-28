using Account.Data.Entity;
using Account.Model.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Account.Data;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DbSet<UserSecurityEntity> UserSecurities => Set<UserSecurityEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().Configure();
        modelBuilder.Entity<UserSecurityEntity>().Configure();

        base.OnModelCreating(modelBuilder);
    }
}