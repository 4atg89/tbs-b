using HeroesService.Grpc;

namespace HeroesService.Heroes;

public abstract class BaseHeroFactory : IHeroFactory
{
    protected abstract HeroStats BaseStats { get; }

    //todo make this method to have ability to pass lvl 
    protected abstract HeroStats GrowthPerLevel { get; }
    protected abstract string Name { get; }
    protected abstract string Image { get; }
    protected abstract string DescriptionTitle { get; }
    protected abstract string Description { get; }
    protected abstract int HeroId { get; }
    protected abstract int Rarity { get; }
    protected abstract int Size { get; }

    public virtual HeroResponseDto BuildHero(int level, int id)
    {
        if (level <= 0) return CreateEmptyHero();

        var stats = CalculateStats(level);

        return new HeroResponseDto
        {
            HeroId = HeroId,
            Name = Name,
            Damage = stats.Damage,
            Health = stats.Health,
            Speed = stats.Speed,
            Weight = stats.Weight,
            Defense = stats.Defense,
            AttackRange = stats.AttackRange,
            Evasion = stats.Evasion,
            Image = Image,
            DescriptionTitle = DescriptionTitle,
            Description = Description,
            NextLevelPriceCoins = CalculateNextLevelPriceCoins(level),
            NextLevelPriceCards = CalculateNextLevelPriceCards(level),
            Rarity = Rarity,
            Size = Size
        };
    }

    protected virtual HeroStats CalculateStats(int level)
    {
        return new HeroStats
        {
            Damage = BaseStats.Damage + (GrowthPerLevel.Damage * (level - 1)),
            Health = BaseStats.Health + (GrowthPerLevel.Health * (level - 1)),
            Speed = BaseStats.Speed + (GrowthPerLevel.Speed * (level - 1)),
            Weight = BaseStats.Weight + (GrowthPerLevel.Weight * (level - 1)),
            Defense = BaseStats.Defense + (GrowthPerLevel.Defense * (level - 1)),
            AttackRange = BaseStats.AttackRange + (GrowthPerLevel.AttackRange * (level - 1)),
            Evasion = BaseStats.Evasion + (GrowthPerLevel.Evasion * (level - 1))
        };
    }

    protected virtual int CalculateNextLevelPriceCoins(int level)
    {
        return 10 + (level * 5);
    }

    protected virtual int CalculateNextLevelPriceCards(int level)
    {
        return 10 + (level * 2);
    }

    protected virtual HeroResponseDto CreateEmptyHero()
    {
        return new HeroResponseDto
        {
            HeroId = HeroId,
            Name = Name,
            Damage = 0,
            Health = 0,
            Speed = 0,
            Weight = 0,
            Defense = 0,
            AttackRange = 0,
            Evasion = 0,
            Image = Image,
            DescriptionTitle = DescriptionTitle,
            Description = Description,
            NextLevelPriceCoins = 10,
            NextLevelPriceCards = 10,
            Rarity = Rarity,
            Size = Size
        };
    }
}

public struct HeroStats
{
    public int Damage { get; set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public int Weight { get; set; }
    public int Defense { get; set; }
    public int AttackRange { get; set; }
    public int Evasion { get; set; }
}