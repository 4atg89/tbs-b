using HeroesService.Grpc;
using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

internal static class HeroesExtensions
{

    internal static HeroesModel MapHeroesModel(this HeroEntity model) =>
        new() { HeroId = model.HeroId, Level = model.Level, HeroCards = model.CardsAmount };

    internal static List<HeroEntity> MapHeroesEntity(this ProfileModel model) =>
        [.. model.Heroes!.Select(h => h.MapHeroesEntity(model.Id))];

    internal static HeroEntity MapHeroesEntity(this HeroesModel model, Guid profileId) =>
        new() { ProfileId = profileId, HeroId = model.HeroId, Level = model.Level, CardsAmount = model.HeroCards };

    internal static HeroesModel MapHeroesModel(this HeroResponseDto model, HeroesModel hero)
    {
        hero.Damage = model.Damage;
        hero.Health = model.Health;
        hero.Speed = model.Speed;
        hero.Weight = model.Weight;
        hero.Defense = model.Defense;
        hero.AttackRange = model.AttackRange;
        hero.Evasion = model.Evasion;
        hero.Image = model.Image;
        hero.Name = model.Name;
        hero.Description = model.Description;
        hero.DescriptionTitle = model.DescriptionTitle;
        hero.NextLevelPriceCoins = model.NextLevelPriceCoins;
        hero.NextLevelPriceCards = model.NextLevelPriceCards;
        hero.Rarity = model.Rarity;
        hero.Size = model.Size;
        return hero;
    }
}