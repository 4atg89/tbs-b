using Microsoft.EntityFrameworkCore;
using Profile.Infrastructure.Configuration;
using Profile.Infrastructure.Entities;

namespace Profile.Infrastructure.Data;

public class ProfileDbContext(DbContextOptions<ProfileDbContext> options) : DbContext(options)
{
    public DbSet<ProfileHandHeroesEntity> ProfileHandHeroes => Set<ProfileHandHeroesEntity>();
    public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
    public DbSet<HeroEntity> Heroes => Set<HeroEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileEntity>().Configure();
        modelBuilder.Entity<ProfileHandHeroesEntity>().Configure();
        modelBuilder.Entity<HeroEntity>().Configure();

        base.OnModelCreating(modelBuilder);
    }
}