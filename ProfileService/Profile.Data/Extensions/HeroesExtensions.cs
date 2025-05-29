using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

internal static class HeroesExtensions
{

    internal static HeroesModel MapHeroesModel(this HeroEntity model) =>
        new() { HeroId = model.HeroId, Level = model.Level, CardsAmount = model.CardsAmount };

    internal static List<HeroEntity> MapHeroesEntity(this ProfileModel model) =>
        [.. model.Heroes!.Select(h => h.MapHeroesEntity(model.Id))];

    internal static HeroEntity MapHeroesEntity(this HeroesModel model, Guid profileId) =>
        new() { ProfileId = profileId, HeroId = model.HeroId, Level = model.Level, CardsAmount = model.CardsAmount };
}