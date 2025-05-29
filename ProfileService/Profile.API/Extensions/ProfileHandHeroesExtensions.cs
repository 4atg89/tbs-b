
using Profile.API.Model;
using Profile.Domain.Model;

namespace Profile.API.Extensions;

internal static class ProfileHandHeroesExtensions
{

    internal static ProfileHandHeroesResponse MapHandHeroesModel(this ProfileHandHeroesModel model) =>
        new() { HeroId = model.HeroId, HandType = model.HandType.MapProfileHandType() };

    internal static DeckHandTypeResponse MapProfileHandType(this DeckHandType model)
    {
        return model switch
        {
            DeckHandType.DYNAMIC => DeckHandTypeResponse.DYNAMIC,
            DeckHandType.REGULAR_1 => DeckHandTypeResponse.REGULAR_1,
            DeckHandType.REGULAR_2 => DeckHandTypeResponse.REGULAR_2,
            DeckHandType.REGULAR_3 => DeckHandTypeResponse.REGULAR_3,
            DeckHandType.REGULAR_4 => DeckHandTypeResponse.REGULAR_4,
            DeckHandType.TOURNAMENT => DeckHandTypeResponse.TOURNAMENT,
            DeckHandType.CHALLENGES => DeckHandTypeResponse.CHALLENGES,
            _ => throw new ArgumentOutOfRangeException(nameof(model), $"Unsupported value: {model}")
        };
    }
    
}