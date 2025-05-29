namespace Profile.Domain.Model;

public class HeroesModel
{
     public required int HeroId { get; set; }
    public required int Level { get; set; }
    public required int CardsAmount { get; set; }

    public static List<HeroesModel> DefaultHeroesList() => new()
    {
        new() { HeroId = 1, Level = 1, CardsAmount = 1},
        new() { HeroId = 2, Level = 1, CardsAmount = 1},
        new() { HeroId = 3, Level = 1, CardsAmount = 1},
        new() { HeroId = 4, Level = 1, CardsAmount = 1},
    };
}