using HeroesService.Grpc;

namespace HeroesService.Heroes;

public class NinjaFactory : IHeroFactory
{
    public HeroResponseDto BuildHero(int level, int id)
    {
        return new HeroResponseDto
        {
            HeroId = 4,
            Name = "Ninja",
            Damage = 30,
            Health = 40,
            Speed = 90,
            Weight = 40,
            Defense = 110,
            AttackRange = 40,
            Evasion = 120,
            Image = "https://img.poki-cdn.com/cdn-cgi/image/quality=78,width=1200,height=1200,fit=cover,f=png/b66cf5d2ede0b1e41b5dfa79dd355f5f.png",
            DescriptionTitle = "Stealth",
            Description = "Hit some ones face till stealth",
            NextLevelPriceCoins = 10,
            NextLevelPriceCards = 20,
            Rarity = 1,
            Size = 1
        };
    }
}