using HeroesService.Grpc;

namespace HeroesService.Heroes;

public class MaskosFactory : IHeroFactory
{
    public HeroResponseDto BuildHero(int level, int id)
    {
        return new HeroResponseDto
        {
            HeroId = 8,
            Name = "Maskos",
            Damage = 20,
            Health = 45,
            Speed = 30,
            Weight = 50,
            Defense = 4,
            AttackRange = 150,
            Evasion = 110,
            Image = "https://static.wikia.nocookie.net/the-mask/images/d/df/The_Mask_of_Loki.jpg/revision/latest/scale-to-width-down/373?cb=20120918222714",
            DescriptionTitle = "Spell",
            Description = "Hit some ones face till spell",
            NextLevelPriceCoins = 8,
            NextLevelPriceCards = 15,
            Rarity = 1,
            Size = 1
        };
    }
}