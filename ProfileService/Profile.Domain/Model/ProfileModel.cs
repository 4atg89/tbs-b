namespace Profile.API.Model;

public class ProfileModel
{
    public required Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required int Rating { get; set; }
    public InventoryModel? Inventory { get; set; }
    public ClanModel? Clan { get; set; }
    public HeroesModel? Heroes { get; set; }
    public MainStatisticsModel? MainStatistics { get; set; }
    public ChallengesModel? Challenges { get; set; }

}

public class InventoryModel
{
    public required int Coins { get; set; }
    public required int Gems { get; set; }
    
}

public class ClanModel
{
    public required Guid Id { get; set; }
    public required string ClanName { get; set; }
}

public class HeroesModel
{
}

public class MainStatisticsModel
{
    public required int Wins { get; set; }
    public required int Rating { get; set; }
    public required int EpicWins { get; set; }
}


public class ChallengesModel
{
    public required int WinStreak { get; set; }
    public required int Count { get; set; }
}

