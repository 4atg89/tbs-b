namespace Profile.Data.Data.Entities;

public class ProfileEntity
{
    public required Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required int Rating { get; set; }
    public required int Coins { get; set; }
    public required int Gems { get; set; }
    public ICollection<HeroEntity>? Heroes { get; set; }
    public Guid? ClanId { get; set; }
    public ICollection<ProfileHandHeroesEntity>? HandHeroes { get; set; }
    public required int MainGamesCount { get; set; }
    public required int MainWinsCount { get; set; }
    public required int MainMaxRating { get; set; }
    public required int MainEpicWinsCount { get; set; }
    public required int MainKilledEnemies { get; set; }
    public required int ChallengeWinStreakCount { get; set; }
    public required int ChallengeWinsCount { get; set; }
    public required int ChallengeGamesCount { get; set; }
}