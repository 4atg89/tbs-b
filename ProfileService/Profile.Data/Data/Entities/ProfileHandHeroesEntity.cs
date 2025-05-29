namespace Profile.Data.Data.Entities;

public class ProfileHandHeroesEntity
{
    public required Guid ProfileId { get; set; }
    public required ProfileHandType HandType { get; set; }
    public required int HeroId { get; set; }
    public HeroEntity? ActiveHeroes { get; set; }
    public ProfileEntity? Profile { get; set; }
}

public enum ProfileHandType
{
    DYNAMIC, REGULAR_1, REGULAR_2, REGULAR_3, REGULAR_4, TOURNAMENT, CHALLENGES
}