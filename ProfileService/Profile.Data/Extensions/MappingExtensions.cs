using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

//todo redo that awful mapping class
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

    internal static List<ProfileHandHeroesModel> MapHandHeroes(this ProfileEntity model) =>
        [.. model.HandHeroes!.Select(h => new ProfileHandHeroesModel() { HeroId = h.HeroId, HandType = h.HandType.MapProfileHandType() })];

    internal static List<ProfileHandHeroesEntity> MapHandHeroes(this ProfileModel model) =>
        [.. model.HandHeroes!.Select(h => h.MapHandHeroes(model.Id))];

    internal static ProfileHandHeroesEntity MapHandHeroes(this ProfileHandHeroesModel model, Guid profileId) =>
        new() { ProfileId = profileId, HeroId = model.HeroId, HandType = model.HandType.MapProfileHandType() };

    internal static List<HeroEntity> MapHeroesEntity(this ProfileModel model) =>
        [.. model.Heroes!.Select(h => h.MapHeroesEntity(model.Id))];

    internal static HeroEntity MapHeroesEntity(this HeroesModel model, Guid profileId) =>
        new() { ProfileId = profileId, HeroId = model.HeroId, Level = model.Level, CardsAmount = model.CardsAmount };

    internal static ProfileHandType MapProfileHandType(this DeckHandType model)
    {
        return model switch
        {
            DeckHandType.DYNAMIC => ProfileHandType.DYNAMIC,
            DeckHandType.REGULAR_1 => ProfileHandType.REGULAR_1,
            DeckHandType.REGULAR_2 => ProfileHandType.REGULAR_2,
            DeckHandType.REGULAR_3 => ProfileHandType.REGULAR_3,
            DeckHandType.REGULAR_4 => ProfileHandType.REGULAR_4,
            DeckHandType.TOURNAMENT => ProfileHandType.TOURNAMENT,
            DeckHandType.CHALLENGES => ProfileHandType.CHALLENGES,
            _ => throw new ArgumentOutOfRangeException(nameof(model), $"Unsupported value: {model}")
        };
    }

    internal static DeckHandType MapProfileHandType(this ProfileHandType model)
    {
        return model switch
        {
            ProfileHandType.DYNAMIC => DeckHandType.DYNAMIC,
            ProfileHandType.REGULAR_1 => DeckHandType.REGULAR_1,
            ProfileHandType.REGULAR_2 => DeckHandType.REGULAR_2,
            ProfileHandType.REGULAR_3 => DeckHandType.REGULAR_3,
            ProfileHandType.REGULAR_4 => DeckHandType.REGULAR_4,
            ProfileHandType.TOURNAMENT => DeckHandType.TOURNAMENT,
            ProfileHandType.CHALLENGES => DeckHandType.CHALLENGES,
            _ => throw new ArgumentOutOfRangeException(nameof(model), $"Unsupported value: {model}")
        };
    }
}