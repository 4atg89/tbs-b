namespace HeroesService.Heroes;

public abstract class Hero
{
    public abstract int Id { get; set; }
    public abstract string Name { get; set; }
    public abstract int Level { get; set; }
    public abstract string Image { get; set; }
    public abstract int Damage { get; set; }
    public abstract int Health { get; set; }
    public abstract int Speed { get; set; }
    public abstract int Weight { get; set; }
    public abstract int Defense { get; set; }
    public abstract int AttackRange { get; set; }
    public abstract int Evasion { get; set; }
    public abstract string DesriptionTitle { get; set; }
    public abstract string Desription { get; set; }
    public abstract int NextLevelPriceCoins { get; set; }
    public abstract int NextLevelPriceCards { get; set; }
    public abstract int Rarity { get; set; }

}