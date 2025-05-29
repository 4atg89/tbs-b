using Profile.API.Model;
using Profile.Domain.Model;

namespace Profile.API.Extensions;

internal static class ChallengesExtensions
{

    internal static ChallengesResponse MapChallenges(this ChallengesModel model) =>
        new() { WinStreak = model.WinStreak, ChallengesCount = model.ChallengesCount, ChallengesWins = model.ChallengesWins };

}