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
            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThTxG3RDV98g63Ujjqst0LvYej8cywueL_RSnq5ku4ft_vMeNXOb0se6gjeeZrbqdZqE4&usqp=CAU",
            DescriptionTitle = "Barbeque",
            Description = "Hit some ones face till barbeque",
            NextLevelPriceCoins = 10,
            NextLevelPriceCards = 10,
            Rarity = 1,
            Size = 1
        };
    }
}