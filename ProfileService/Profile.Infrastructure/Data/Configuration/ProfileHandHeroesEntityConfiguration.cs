using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Infrastructure.Entities;

namespace Profile.Infrastructure.Configuration;

public static class ProfileHandHeroesEntityConfiguration
{

    public static void Configure(this EntityTypeBuilder<ProfileHandHeroesEntity> entity)
    {
        entity.ToTable("profile_hand_heroes");

        entity.HasKey(e => new { e.ProfileId, e.HandType, e.HeroId });

        entity.Property(x => x.ProfileId).HasColumnName("profile_id").IsRequired();

        entity.Property(x => x.HeroId).HasColumnName("hero_id").IsRequired();

        entity.Property(x => x.HandType).HasColumnName("type").IsRequired();

        entity.HasOne(x => x.Profile)
            .WithMany(p => p.ActiveHeroes)
            .HasForeignKey(x => x.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}