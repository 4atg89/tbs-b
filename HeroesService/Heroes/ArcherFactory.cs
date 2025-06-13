namespace HeroesService.Heroes;

public class ArcherFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 10,
        Health = 30,
        Speed = 80,
        Weight = 50,
        Defense = 2,
        AttackRange = 150,
        Evasion = 110
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 2,
        Health = 5,
        Speed = 2,
        Weight = 1,
        Defense = 1,
        AttackRange = 3,
        Evasion = 3
    };

    protected override string Name => "Archer";
    protected override string Image => "https://static.tvtropes.org/pmwiki/pub/images/19924b5f_c3b7_4fc0_bb4a_efe231d6f5b9.jpeg";
    protected override string DescriptionTitle => "Arrow";
    protected override string Description => "Hit some ones face till arrow";
    protected override int HeroId => 2;
    protected override int Rarity => 1;
    protected override int Size => 1;
}