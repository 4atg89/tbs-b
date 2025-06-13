namespace HeroesService.Heroes;

public class TrollFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 25,
        Health = 60,
        Speed = 20,
        Weight = 80,
        Defense = 15,
        AttackRange = 50,
        Evasion = 5
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 3,
        Health = 10,
        Speed = 1,
        Weight = 3,
        Defense = 3,
        AttackRange = 1,
        Evasion = 1
    };

    protected override string Name => "Troll";
    protected override string Image => "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHBAlM00zZs6-SgzHAIoVEt8R5OSxQ-KMdtA&s";
    protected override string DescriptionTitle => "Spell";
    protected override string Description => "Hit some ones face till spell";
    protected override int HeroId => 10;
    protected override int Rarity => 1;
    protected override int Size => 1;
}