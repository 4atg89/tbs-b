using HeroesService.Grpc;

namespace HeroesService.Heroes;

public class NinjaFactory : IHeroFactory
{
    public HeroResponseDto BuildHero(int level)
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
            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThTxG3RDV98g63Ujjqst0LvYej8cywueL_RSnq5ku4ft_vMeNXOb0se6gjeeZrbqdZqE4&usqp=CAU",
            DescriptionTitle = "Stealth",
            Description = "Hit some ones face till stealth",
            NextLevelPriceCoins = 10,
            NextLevelPriceCards = 20,
            Rarity = 1
        };
    }
}