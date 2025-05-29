using Profile.API.Model;
using Profile.Data.Data.Entities;

namespace Profile.Data.Extensions;

internal static class MappingExtensions
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
            Heroes = model.MapHeroesEntity()
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
            Challenges = model.MapChallenges()
        };
    }

    internal static InventoryModel MapInventory(this ProfileEntity model) =>
        new() { Coins = model.Coins, Gems = model.Gems };

    internal static MainStatisticsModel MapMainStatistics(this ProfileEntity model) =>
        new() { Wins = model.MainWinsCount, MaxRating = model.MainMaxRating, EpicWins = model.MainEpicWinsCount, GamesCount = model.MainGamesCount, KilledEnemies = model.MainKilledEnemies };

    internal static HeroesModel MapHeroesModel(this HeroEntity model) =>
        new() { HeroId = model.HeroId, Level = model.Level, CardsAmount = model.CardsAmount };

    internal static ChallengesModel MapChallenges(this ProfileEntity model) =>
        new() { WinStreak = model.ChallengeWinStreakCount, ChallengesCount = model.ChallengeGamesCount, ChallengesWins = model.ChallengeWinsCount };

    internal static ClanModel? MapClan(this ProfileEntity model)
    {
        var clanId = model.ClanId;
        if (clanId == null) return null;
        return new() { Id = (Guid)clanId };
    }

    internal static List<HeroEntity> MapHeroesEntity(this ProfileModel model) =>
        [.. model.Heroes!.Select(h => h.MapHeroesEntity(model.Id))];

    internal static HeroEntity MapHeroesEntity(this HeroesModel model, Guid profileId) =>
        new() { ProfileId = profileId, HeroId = model.HeroId, Level = model.Level, CardsAmount = model.CardsAmount };
}