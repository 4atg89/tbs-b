using HeroesService.Grpc;

namespace HeroesService.Heroes;

public class BarbarianFactory: IHeroFactory
{
    public HeroResponseDto BuildHero(int level, int id)
    {
        return new HeroResponseDto
        {
            HeroId = 1,
            Name = "Barbarian",
            Damage = 20,
            Health = 50,
            Speed = 40,
            Weight = 70,
            Defense = 10,
            AttackRange = 50,
            Evasion = 10,
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/Conan_colors_by_rodrigokatrakas_ddcrjw1-fullview.jpg/250px-Conan_colors_by_rodrigokatrakas_ddcrjw1-fullview.jpg",
            DescriptionTitle = "Barbeque",
            Description = "Hit some ones face till barbeque",
            NextLevelPriceCoins = 10,
            NextLevelPriceCards = 10,
            Rarity = 1,
            Size = 1
        };
    }
}