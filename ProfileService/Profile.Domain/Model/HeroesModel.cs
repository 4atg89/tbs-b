namespace Profile.Domain.Model;

public class HeroesModel
{
    public required int HeroId { get; set; }
    public required int Level { get; set; }
    public required int HeroCards { get; set; }
    public int Damage { get; set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public int Weight { get; set; }
    public int Defense { get; set; }
    public int AttackRange { get; set; }
    public int Evasion { get; set; }
    public string? Image { get; set; }
    public string? Name { get; set; }
    public string? DescriptionTitle { get; set; }
    public string? Description { get; set; }
    public int? NextLevelPriceCoins { get; set; }
    public int? NextLevelPriceCards { get; set; }
    public int? Rarity { get; set; }
    public int? Size { get; set; }

    // todo redo when ui will be clear
    public static HeroesModel DefaultEmptyHero(
        int id,
        string image,
        string name,
        string description,
        string descriptionTitle,
        int cardsAmount = 0,
        int level = 0,
        int nextLevelPriceCoins = 0,
        int nextLevelPriceGumsters = 0
    ) => new()
    {
        HeroId = id,
        HeroCards = cardsAmount,
        Level = level,
        Image = image,
        Damage = 0,
        Health = 0,
        Speed = 0,
        Weight = 0,
        Defense = 0,
        AttackRange = 0,
        Evasion = 0,
        Name = name,
        Description = description,
        DescriptionTitle = descriptionTitle,
        NextLevelPriceCoins = nextLevelPriceCoins,
        NextLevelPriceCards = nextLevelPriceGumsters,
        Rarity = 0,
        Size = 0
    };

    public static List<HeroesModel> DefaultHeroesList() => new()
    {
        new() { HeroId = 1, Level = 1, HeroCards = 1},
        new() { HeroId = 2, Level = 1, HeroCards = 1},
        new() { HeroId = 3, Level = 1, HeroCards = 1},
        new() { HeroId = 4, Level = 1, HeroCards = 1},
    };
}