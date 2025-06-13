namespace HeroesService.Heroes;

public class MageFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 25,
        Health = 35,
        Speed = 30,
        Weight = 50,
        Defense = 4,
        AttackRange = 150,
        Evasion = 110
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 4,
        Health = 4,
        Speed = 1,
        Weight = 1,
        Defense = 1,
        AttackRange = 2,
        Evasion = 2
    };

    protected override string Name => "Mage";
    protected override string Image => "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDBwtYbzCna20BTEUMmnHlUL9UWqo3agIl6Q&s";
    protected override string DescriptionTitle => "Spell";
    protected override string Description => "Hit some ones face till spell";
    protected override int HeroId => 3;
    protected override int Rarity => 1;
    protected override int Size => 1;
}