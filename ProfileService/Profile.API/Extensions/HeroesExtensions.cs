
using Profile.API.Model;
using Profile.Domain.Model;

namespace Profile.API.Extensions;

internal static class HeroesExtensions
{

    internal static HeroesResponse MapHeroesModel(this HeroesModel model) =>
        new()
        {
            HeroId = model.HeroId,
            Level = model.Level,
            CardsAmount = model.CardsAmount,
            Image = model.Image,
            NextLevelPriceCoins = model.NextLevelPriceCoins
        };

}