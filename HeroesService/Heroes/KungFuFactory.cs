namespace HeroesService.Heroes;

public class KungFuFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 25,
        Health = 45,
        Speed = 60,
        Weight = 50,
        Defense = 4,
        AttackRange = 150,
        Evasion = 110
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 3,
        Health = 6,
        Speed = 2,
        Weight = 1,
        Defense = 2,
        AttackRange = 2,
        Evasion = 3
    };

    protected override string Name => "KungFu";
    protected override string Image => "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcaRj8tSnoQjW8nkL5kbOWvMkxEa3PqKmBNQ&s";
    protected override string DescriptionTitle => "Spell";
    protected override string Description => "Hit some ones face till spell";
    protected override int HeroId => 5;
    protected override int Rarity => 1;
    protected override int Size => 1;
}