namespace Profile.Infrastructure.Entities;

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
    REGULAR, TOURNAMENT, CHALLENGES
}