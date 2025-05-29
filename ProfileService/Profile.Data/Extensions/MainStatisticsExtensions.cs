using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

internal static class MainStatisticsExtensions
{

    internal static MainStatisticsModel MapMainStatistics(this ProfileEntity model) =>
        new() { Wins = model.MainWinsCount, MaxRating = model.MainMaxRating, EpicWins = model.MainEpicWinsCount, GamesCount = model.MainGamesCount, KilledEnemies = model.MainKilledEnemies };

}