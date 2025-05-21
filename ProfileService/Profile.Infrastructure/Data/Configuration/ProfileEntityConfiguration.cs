using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Infrastructure.Entities;

namespace Profile.Infrastructure.Configuration;

public static class ProfileEntityConfiguration
{

    public static void Configure(this EntityTypeBuilder<ProfileEntity> entity)
    {
        entity.ToTable("profiles");

        entity.HasKey(e => e.Id);
        entity.Property(x => x.Id).HasColumnName("id").IsRequired();

        entity.Property(x => x.Nickname).HasColumnName("nickname").HasMaxLength(50).IsRequired();

        entity.Property(x => x.Rating).HasColumnName("rating").IsRequired();

        entity.Property(x => x.Coins).HasColumnName("coins").IsRequired();

        entity.Property(x => x.Gems).HasColumnName("gems").IsRequired();

        entity.Property(x => x.ClanId).HasColumnName("clan_id").IsRequired();

        entity.Property(x => x.WinsCount).HasColumnName("wins_count").IsRequired();

        entity.Property(x => x.MaxRating).HasColumnName("max_rating").IsRequired();

        entity.Property(x => x.EpicWinsCount).HasColumnName("epic_wins_count").IsRequired();

        entity.Property(x => x.WinStreakCount).HasColumnName("win_streak_count").IsRequired();

        entity.Property(x => x.BattleCount).HasColumnName("battle_count").IsRequired();

        entity.HasIndex(e => e.Nickname).IsUnique();

        entity.HasIndex(e => e.ClanId);
    }
}