namespace HeroesService.Heroes;

public class MaskosFactory : BaseHeroFactory
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

    protected override string Name => "Maskos";
    protected override string Image => "https://static.wikia.nocookie.net/the-mask/images/d/df/The_Mask_of_Loki.jpg/revision/latest/scale-to-width-down/373?cb=20120918222714";
    protected override string DescriptionTitle => "Spell";
    protected override string Description => "Hit some ones face till spell";
    protected override int HeroId => 8;
    protected override int Rarity => 1;
    protected override int Size => 1;
}