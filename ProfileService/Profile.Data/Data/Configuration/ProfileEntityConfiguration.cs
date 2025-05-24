using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.Data.Data.Entities;

namespace Profile.Data.Data.Configuration;

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

        entity.Property(x => x.ClanId).HasColumnName("clan_id");

        entity.Property(x => x.MainWinsCount).HasColumnName("main_wins_count").IsRequired();

        entity.Property(x => x.MainMaxRating).HasColumnName("main_max_rating").IsRequired();

        entity.Property(x => x.MainGamesCount).HasColumnName("main_games_count").IsRequired();

        entity.Property(x => x.MainEpicWinsCount).HasColumnName("main_epic_wins_count").IsRequired();

        entity.Property(x => x.MainKilledEnemies).HasColumnName("main_killed_enemies_count").IsRequired();

        entity.Property(x => x.ChallengeWinStreakCount).HasColumnName("challenge_win_streak_count").IsRequired();

        entity.Property(x => x.ChallengeWinsCount).HasColumnName("challenge_wins_count").IsRequired();

        entity.Property(x => x.ChallengeGamesCount).HasColumnName("challenge_games_count").IsRequired();

        entity.HasIndex(e => e.Nickname).IsUnique();

        entity.HasIndex(e => e.ClanId);
    }
}