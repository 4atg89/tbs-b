using HeroesService.Grpc;

namespace HeroesService.Heroes;

public class ArcherFactory: IHeroFactory
{
    public HeroResponseDto BuildHero(int level, int id)
    {
        return new HeroResponseDto
        {
            HeroId = 2,
            Name = "Archer",
            Damage = 10,
            Health = 30,
            Speed = 80,
            Weight = 50,
            Defense = 2,
            AttackRange = 150,
            Evasion = 110,
            Image = "https://static.tvtropes.org/pmwiki/pub/images/19924b5f_c3b7_4fc0_bb4a_efe231d6f5b9.jpeg",
            DescriptionTitle = "Arrow",
            Description = "Hit some ones face till arrow",
            NextLevelPriceCoins = 9,
            NextLevelPriceCards = 11,
            Rarity = 1,
            Size = 1
        };
    }
}