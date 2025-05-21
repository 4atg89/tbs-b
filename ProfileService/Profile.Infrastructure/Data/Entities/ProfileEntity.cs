namespace Profile.Infrastructure.Entities;

public class ProfileEntity
{
    public required Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required int Rating { get; set; }
    public required int Coins { get; set; }
    public required int Gems { get; set; }
    public ICollection<HeroEntity>? Heroes { get; set; }
    public Guid? ClanId { get; set; }
    public ICollection<ProfileHandHeroesEntity>? ActiveHeroes { get; set; }
    public required int WinsCount { get; set; }
    public required int MaxRating { get; set; }
    public required int EpicWinsCount { get; set; }
    public required int WinStreakCount { get; set; }
    public required int BattleCount { get; set; }
}