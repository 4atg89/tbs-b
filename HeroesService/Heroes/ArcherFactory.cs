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
            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThTxG3RDV98g63Ujjqst0LvYej8cywueL_RSnq5ku4ft_vMeNXOb0se6gjeeZrbqdZqE4&usqp=CAU",
            DescriptionTitle = "Arrow",
            Description = "Hit some ones face till arrow",
            NextLevelPriceCoins = 9,
            NextLevelPriceCards = 11,
            Rarity = 1,
            Size = 1
        };
    }
}