using Auth.Data.Entity;
using Auth.Model.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
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