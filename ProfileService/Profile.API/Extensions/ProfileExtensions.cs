using Profile.API.Model;
using Profile.Domain.Model;

namespace Profile.API.Extensions;

internal static class ProfileExtensions
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
            HandHeroes = [.. model.HandHeroes!.Select(h => h.MapHandHeroesModel())],
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


    private static ClanResponse? MapClan(ClanModel? model)
    {
        if (model == null) return null;
        return new ClanResponse
        {
            Id = model.Id,
            ClanName = model.ClanName!
        };
    }
}