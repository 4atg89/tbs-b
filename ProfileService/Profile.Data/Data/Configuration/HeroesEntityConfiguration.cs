using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Data.Data.Entities;

namespace Profile.Data.Data.Configuration;

public static class HeroesEntityConfiguration
{

    public static void Configure(this EntityTypeBuilder<HeroEntity> entity)
    {
        entity.ToTable("profile_heroes");

        entity.HasKey(e => new { e.ProfileId, e.HeroId });

        entity.Property(e => e.ProfileId).HasColumnName("profile_id").IsRequired();

        entity.Property(e => e.HeroId).HasColumnName("hero_id").IsRequired();

        entity.Property(e => e.Level).HasColumnName("level").IsRequired();
        
        entity.Property(e => e.CardsAmount).HasColumnName("cards_amount").IsRequired();

        entity.HasOne(e => e.Profile)
            .WithMany(i => i.Heroes)
            .HasForeignKey(e => e.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}