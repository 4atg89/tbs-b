using HeroesService.Grpc;

namespace HeroesService.Heroes;

public class GrimFactory : IHeroFactory
{
    public HeroResponseDto BuildHero(int level, int id)
    {
        return new HeroResponseDto
        {
            HeroId = 9,
            Name = "Grim",
            Damage = 20,
            Health = 45,
            Speed = 30,
            Weight = 50,
            Defense = 4,
            AttackRange = 150,
            Evasion = 110,
            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ2V5oIVYDnoipYvX-hkTWzlk5iHrSrH1nPDg&s",
            DescriptionTitle = "Spell",
            Description = "Hit some ones face till spell",
            NextLevelPriceCoins = 8,
            NextLevelPriceCards = 15,
            Rarity = 1,
            Size = 1
        };
    }
}