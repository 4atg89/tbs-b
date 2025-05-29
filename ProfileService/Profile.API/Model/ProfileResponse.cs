namespace Profile.API.Model;

public class ProfileResponse
{
    public required Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required int Rating { get; set; }
    public InventoryResponse? Inventory { get; set; }
    public ClanResponse? Clan { get; set; }
    public List<HeroesResponse>? Heroes { get; set; }
    public List<ProfileHandHeroesResponse>? HandHeroes { get; set; }
    public MainStatisticsResponse? MainStatistics { get; set; }
    public ChallengesResponse? Challenges { get; set; }

}

public class InventoryResponse
{
    public required int Coins { get; set; }
    public required int Gems { get; set; }

}

public class ClanResponse
{

    public required Guid Id { get; set; }
    public required string ClanName { get; set; }
}

public class HeroesResponse
{
    public required int HeroId { get; set; }
    public required int Level { get; set; }
    public required int CardsAmount { get; set; }
}

public class ProfileHandHeroesResponse
{
    public required DeckHandTypeResponse HandType { get; set; }
    public required int HeroId { get; set; }
}

public enum DeckHandTypeResponse
{
    DYNAMIC, REGULAR_1, REGULAR_2, REGULAR_3, REGULAR_4, TOURNAMENT, CHALLENGES
}

public class MainStatisticsResponse
{

    public required int Wins { get; set; }
    public required int MaxRating { get; set; }
    public required int EpicWins { get; set; }
    public required int GamesCount { get; set; }
    public required int KilledEnemies { get; set; }
}


public class ChallengesResponse
{

    public required int WinStreak { get; set; }
    public required int ChallengesCount { get; set; }
    public required int ChallengesWins { get; set; }
}

