namespace HeroesService.Heroes;

public class NinjaFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 30,
        Health = 40,
        Speed = 90,
        Weight = 40,
        Defense = 110,
        AttackRange = 40,
        Evasion = 120
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 3,
        Health = 5,
        Speed = 3,
        Weight = 1,
        Defense = 4,
        AttackRange = 1,
        Evasion = 4
    };

    protected override string Name => "Ninja";
    protected override string Image => "https://img.poki-cdn.com/cdn-cgi/image/quality=78,width=1200,height=1200,fit=cover,f=png/b66cf5d2ede0b1e41b5dfa79dd355f5f.png";
    protected override string DescriptionTitle => "Stealth";
    protected override string Description => "Hit some ones face till stealth";
    protected override int HeroId => 4;
    protected override int Rarity => 1;
    protected override int Size => 1;
}