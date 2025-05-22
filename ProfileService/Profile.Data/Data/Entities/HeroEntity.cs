namespace Profile.Data.Data.Entities;

public class HeroEntity
{
    public required Guid ProfileId { get; set; }
    public required int HeroId { get; set; }
    public required int Level { get; set; }
    public ProfileEntity? Profile { get; set; }
    public ICollection<ProfileHandHeroesEntity>? HandHeroes { get; set; }
}