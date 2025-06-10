using HeroesService.Grpc;

namespace HeroesService.Heroes;

public class GhostFactory : IHeroFactory
{
    public HeroResponseDto BuildHero(int level, int id)
    {
        return new HeroResponseDto
        {
            HeroId = 6,
            Name = "Ghost",
            Damage = 20,
            Health = 45,
            Speed = 30,
            Weight = 50,
            Defense = 4,
            AttackRange = 150,
            Evasion = 110,
            Image = "https://static.vecteezy.com/system/resources/thumbnails/050/615/820/small_2x/a-ghostly-figure-in-a-white-ghost-costume-with-a-black-background-video.jpg",
            DescriptionTitle = "Spell",
            Description = "Hit some ones face till spell",
            NextLevelPriceCoins = 8,
            NextLevelPriceCards = 15,
            Rarity = 1,
            Size = 1
        };
    }
}