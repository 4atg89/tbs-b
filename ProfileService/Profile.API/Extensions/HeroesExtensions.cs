
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
            HeroCards = model.HeroCards,
            Damage = model.Damage,
            Health = model.Health,
            Speed = model.Speed,
            Weight = model.Weight,
            Defense = model.Defense,
            AttackRange = model.AttackRange,
            Evasion = model.Evasion,
            Image = model.Image,
            Name = model.Name,
            Description = model.Description,
            DescriptionTitle = model.DescriptionTitle,
            NextLevelPriceCoins = model.NextLevelPriceCoins,
            NextLevelPriceCards = model.NextLevelPriceCards,
            Rarity = model.Rarity,
            Size = model.Size
        };

}