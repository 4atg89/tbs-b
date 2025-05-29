using Profile.API.Model;

namespace Profile.API.Extensions;

internal static class MappingExtensions
{

    internal static ProfileResponse MapProfile(this ProfileModel model)
    {
        return new ProfileResponse
        {
            Id = model.Id,
            Nickname = model.Nickname,
            Rating = model.Rating,
        };
    }

    internal static ProfileResponse MapUserProfile(this ProfileModel model)
    {
        var inventory = model.Inventory ?? throw new ArgumentNullException($"Inventory can not be null {model.Id}");
        return new ProfileResponse
        {
            Id = model.Id,
            Nickname = model.Nickname,
            Rating = model.Rating,
            Inventory = inventory.MapInventory()
        };
    }

    private static ClanResponse? MapClan(ClanModel? model)
    {
        if (model == null) return null;
        return new ClanResponse
        {
            Id = model.Id,
            ClanName = model.ClanName!
        };
    }
    internal static ProfileResponse MapUserProfileDetails(this ProfileModel model)
    {
        var inventory = model.Inventory ?? throw new ArgumentNullException($"Inventory can not be null {model.Id}");
        var mainStatistics = model.MainStatistics ?? throw new ArgumentNullException($"MainStatistics can not be null {model.Id}");
        var challenges = model.Challenges ?? throw new ArgumentNullException($"Challenges can not be null {model.Id}");
        return new ProfileResponse
        {
            Id = model.Id,
            Nickname = model.Nickname,
            Rating = model.Rating,
            Inventory = inventory.MapInventory(),
            Clan = MapClan(model.Clan),
            Heroes = [.. model.Heroes!.Select(h => h.MapHeroesModel())],
            MainStatistics = mainStatistics.MapMainStatistics(),
            Challenges = challenges.MapChallenges()
        };
    }

    internal static ProfileResponse MapProfileDetails(this ProfileModel model)
    {
        var mainStatistics = model.MainStatistics ?? throw new ArgumentNullException($"MainStatistics can not be null {model.Id}");
        var challenges = model.Challenges ?? throw new ArgumentNullException($"Challenges can not be null {model.Id}");

        return new ProfileResponse
        {
            Id = model.Id,
            Nickname = model.Nickname,
            Rating = model.Rating,
            Clan = MapClan(model.Clan),
            Heroes = [.. model.Heroes!.Select(h => h.MapHeroesModel())],
            MainStatistics = mainStatistics.MapMainStatistics(),
            Challenges = challenges.MapChallenges()
        };
    }


    internal static InventoryResponse MapInventory(this InventoryModel model) =>
        new() { Coins = model.Coins, Gems = model.Gems };

    internal static MainStatisticsResponse MapMainStatistics(this MainStatisticsModel model) =>
        new() { Wins = model.Wins, MaxRating = model.MaxRating, EpicWins = model.EpicWins, GamesCount = model.GamesCount, KilledEnemies = model.KilledEnemies };

    internal static HeroesResponse MapHeroesModel(this HeroesModel model) =>
        new() { HeroId = model.HeroId, Level = model.Level, CardsAmount = model.CardsAmount };

    internal static ChallengesResponse MapChallenges(this ChallengesModel model) =>
        new() { WinStreak = model.WinStreak, ChallengesCount = model.ChallengesCount, ChallengesWins = model.ChallengesWins };

}