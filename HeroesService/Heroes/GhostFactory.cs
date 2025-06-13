namespace HeroesService.Heroes;

public class GhostFactory : BaseHeroFactory
{
    protected override HeroStats BaseStats => new()
    {
        Damage = 20,
        Health = 45,
        Speed = 70,
        Weight = 30,
        Defense = 4,
        AttackRange = 150,
        Evasion = 110
    };

    protected override HeroStats GrowthPerLevel => new()
    {
        Damage = 2,
        Health = 5,
        Speed = 3,
        Weight = 1,
        Defense = 1,
        AttackRange = 2,
        Evasion = 4
    };

    protected override string Name => "Ghost";
    protected override string Image => "https://static.vecteezy.com/system/resources/thumbnails/050/615/820/small_2x/a-ghostly-figure-in-a-white-ghost-costume-with-a-black-background-video.jpg";
    protected override string DescriptionTitle => "Spell";
    protected override string Description => "Hit some ones face till spell";
    protected override int HeroId => 6;
    protected override int Rarity => 1;
    protected override int Size => 1;
}