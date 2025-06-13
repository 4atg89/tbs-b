namespace HeroesService.Heroes;

public class BarbarianFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 20,
        Health = 50,
        Speed = 40,
        Weight = 70,
        Defense = 10,
        AttackRange = 50,
        Evasion = 10
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 3,
        Health = 8,
        Speed = 1,
        Weight = 2,
        Defense = 2,
        AttackRange = 1,
        Evasion = 1
    };

    protected override string Name => "Barbarian";
    protected override string Image => "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/Conan_colors_by_rodrigokatrakas_ddcrjw1-fullview.jpg/250px-Conan_colors_by_rodrigokatrakas_ddcrjw1-fullview.jpg";
    protected override string DescriptionTitle => "Barbeque";
    protected override string Description => "Hit some ones face till barbeque";
    protected override int HeroId => 1;
    protected override int Rarity => 1;
    protected override int Size => 1;
}