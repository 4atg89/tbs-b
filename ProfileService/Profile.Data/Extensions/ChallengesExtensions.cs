using Profile.Data.Data.Entities;
using Profile.Domain.Model;

namespace Profile.Data.Extensions;

internal static class ChallengesExtensions
{

    internal static ChallengesModel MapChallenges(this ProfileEntity model) =>
        new() { WinStreak = model.ChallengeWinStreakCount, ChallengesCount = model.ChallengeGamesCount, ChallengesWins = model.ChallengeWinsCount };

}