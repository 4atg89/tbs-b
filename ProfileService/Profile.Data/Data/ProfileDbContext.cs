using Microsoft.EntityFrameworkCore;
using Profile.Data.Data.Configuration;
using Profile.Data.Data.Entities;

namespace Profile.Data.Data;

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