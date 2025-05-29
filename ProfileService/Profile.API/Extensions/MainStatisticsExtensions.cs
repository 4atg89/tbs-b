
using Profile.API.Model;
using Profile.Domain.Model;

namespace Profile.API.Extensions;

internal static class MainStatisticsExtensions
{

    internal static MainStatisticsResponse MapMainStatistics(this MainStatisticsModel model) =>
        new()
        {
            Wins = model.Wins,
            MaxRating = model.MaxRating,
            EpicWins = model.EpicWins,
            GamesCount = model.GamesCount,
            KilledEnemies = model.KilledEnemies
        };

}