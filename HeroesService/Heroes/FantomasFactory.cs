namespace HeroesService.Heroes;

public class FantomasFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 20,
        Health = 45,
        Speed = 30,
        Weight = 50,
        Defense = 4,
        AttackRange = 150,
        Evasion = 110
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 2,
        Health = 5,
        Speed = 1,
        Weight = 1,
        Defense = 1,
        AttackRange = 2,
        Evasion = 2
    };

    protected override string Name => "Fantomas";
    protected override string Image => "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxyCoJExamlw1QczUzRgffbFrKn2riJGXHmg&s";
    protected override string DescriptionTitle => "Spell";
    protected override string Description => "Hit some ones face till spell";
    protected override int HeroId => 7;
    protected override int Rarity => 1;
    protected override int Size => 1;
}