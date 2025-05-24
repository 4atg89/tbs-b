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

    public static ProfileModel DefaultProfile(Guid id, string nickname)
    {
        return new ProfileModel
        {
            Id = id,
            Nickname = nickname,
            Rating = 0,
            Inventory = InventoryModel.DefaultInventory(),
            Clan = null,
            Heroes = new HeroesModel { },
            MainStatistics = MainStatisticsModel.DefaultMainStatistics(),
            Challenges = ChallengesModel.DefaultChallenges()
        };
    }
}

public class InventoryModel
{
    public required int Coins { get; set; }
    public required int Gems { get; set; }
    public static InventoryModel DefaultInventory() => new InventoryModel { Coins = 200, Gems = 200 };

}

public class ClanModel
{
    public required Guid Id { get; set; }
    public string? ClanName { get; set; }

}

public class HeroesModel
{
}

public class MainStatisticsModel
{
    public required int Wins { get; set; }
    public required int MaxRating { get; set; }
    public required int EpicWins { get; set; }
    public required int GamesCount { get; set; }
    public required int KilledEnemies { get; set; }
    public static MainStatisticsModel DefaultMainStatistics() => new MainStatisticsModel { Wins = 0, MaxRating = 0, EpicWins = 0, GamesCount = 0, KilledEnemies = 0 };
}


public class ChallengesModel
{
    public required int WinStreak { get; set; }
    public required int ChallengesCount { get; set; }
    public required int ChallengesWins { get; set; }
    public static ChallengesModel DefaultChallenges() => new ChallengesModel { WinStreak = 0, ChallengesCount = 0, ChallengesWins = 0 };
}

