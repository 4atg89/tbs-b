namespace Profile.API.Model;

public class ProfileResponse
{
    public required Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required int Rating { get; set; }
    public InventoryResponse? Inventory { get; set; }
    public ClanResponse? Clan { get; set; }
    public HeroesResponse? Heroes { get; set; }
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
}

public class MainStatisticsResponse
{
    public required int Wins { get; set; }
    public required int Rating { get; set; }
    public required int EpicWins { get; set; }
}


public class ChallengesResponse
{
    public required int WinStreak { get; set; }
    public required int Count { get; set; }
}

