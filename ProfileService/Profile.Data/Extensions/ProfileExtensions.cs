using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

internal static class ProfileExtensions
{

    internal static ProfileEntity MapProfile(this ProfileModel model)
    {
        var inventory = model.Inventory ?? throw new ArgumentNullException($"Inventory can not be null {model.Id}");
        var mainStatistics = model.MainStatistics ?? throw new ArgumentNullException($"MainStatistics can not be null {model.Id}");
        var challenges = model.Challenges ?? throw new ArgumentNullException($"Challenges can not be null {model.Id}");
        return new ProfileEntity
        {
            Id = model.Id,
            Nickname = model.Nickname,
            Rating = model.Rating,
            Coins = inventory.Coins,
            Gems = inventory.Gems,
            ClanId = model.Clan?.Id,
            MainWinsCount = mainStatistics.Wins,
            MainMaxRating = mainStatistics.MaxRating,
            MainEpicWinsCount = mainStatistics.EpicWins,
            MainGamesCount = mainStatistics.GamesCount,
            MainKilledEnemies = mainStatistics.KilledEnemies,
            ChallengeWinStreakCount = challenges.WinStreak,
            ChallengeWinsCount = challenges.ChallengesCount,
            ChallengeGamesCount = challenges.ChallengesWins,
            Heroes = model.MapHeroesEntity(),
            HandHeroes = model.MapHandHeroes()
        };
    }

    internal static ProfileModel MapProfile(this ProfileEntity model)
    {
        return new ProfileModel
        {
            Id = model.Id,
            Nickname = model.Nickname,
            Rating = model.Rating,
            Inventory = model.MapInventory(),
            Clan = model.MapClan(),
            Heroes = [.. model.Heroes!.Select(h => h.MapHeroesModel())],
            MainStatistics = model.MapMainStatistics(),
            Challenges = model.MapChallenges(),
            HandHeroes = model.MapHandHeroes()
        };
    }

    internal static ClanModel? MapClan(this ProfileEntity model)
    {
        var clanId = model.ClanId;
        if (clanId == null) return null;
        return new() { Id = (Guid)clanId };
    }

}