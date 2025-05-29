namespace Profile.Domain.Model;

public class ProfileHandHeroesModel
{
    public required DeckHandType HandType { get; set; }
    public required int HeroId { get; set; }

    public static List<ProfileHandHeroesModel> DefaultHeroesList(List<HeroesModel> heroes)
    {
        var types = Enum.GetValues<DeckHandType>();
        int capacity = heroes.Count * types.Length;

        var result = new List<ProfileHandHeroesModel>(capacity);

        foreach (var type in types)
        {
            foreach (var hero in heroes)
            {
                result.Add(new ProfileHandHeroesModel { HeroId = hero.HeroId, HandType = type });
            }
        }

        return result;
    }

}

public enum DeckHandType
{
    DYNAMIC, REGULAR_1, REGULAR_2, REGULAR_3, REGULAR_4, TOURNAMENT, CHALLENGES
}