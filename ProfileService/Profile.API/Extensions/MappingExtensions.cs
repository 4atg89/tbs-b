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
            Inventory = new InventoryResponse
            {
                Coins = inventory.Coins,
                Gems = inventory.Gems
            }
        };
    }

    private static ClanResponse MapClan(ClanModel? model)
    {
        if (model == null) return null;
        return new ClanResponse
        {
            Id = model.Id,
            ClanName = model.ClanName
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
            Inventory = new InventoryResponse
            {
                Coins = inventory.Coins,
                Gems = inventory.Gems
            },
            Clan = MapClan(model.Clan),
            Heroes = new HeroesResponse
            {

            },
            MainStatistics = new MainStatisticsResponse
            {
                Wins = mainStatistics.Wins,
                Rating = mainStatistics.MaxRating,
                EpicWins = mainStatistics.EpicWins,

            },
            Challenges = new ChallengesResponse
            {
                WinStreak = challenges.WinStreak,
                Count = challenges.ChallengesCount
            }
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
            Heroes = new HeroesResponse
            {

            },
            MainStatistics = new MainStatisticsResponse
            {
                Wins = mainStatistics.Wins,
                Rating = mainStatistics.MaxRating,
                EpicWins = mainStatistics.EpicWins,

            },
            Challenges = new ChallengesResponse
            {
                WinStreak = challenges.WinStreak,
                Count = challenges.ChallengesCount
            }
        };
    }
}