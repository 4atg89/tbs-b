using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

internal static class ProfileHandHeroesExtensions
{


    internal static List<ProfileHandHeroesModel> MapHandHeroes(this ProfileEntity model) =>
        [.. model.HandHeroes!.Select(h => new ProfileHandHeroesModel() { HeroId = h.HeroId, HandType = h.HandType.MapProfileHandType() })];

    internal static List<ProfileHandHeroesEntity> MapHandHeroes(this ProfileModel model) =>
        [.. model.HandHeroes!.Select(h => h.MapHandHeroes(model.Id))];

    internal static ProfileHandHeroesEntity MapHandHeroes(this ProfileHandHeroesModel model, Guid profileId) =>
        new() { ProfileId = profileId, HeroId = model.HeroId, HandType = model.HandType.MapProfileHandType() };
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