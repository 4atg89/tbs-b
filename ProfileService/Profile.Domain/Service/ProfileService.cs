using Profile.Domain.Model;
using Profile.Domain.Repository;

namespace Profile.Domain;

internal class ProfileService(IProfileRepository repository, IHeroesGRPCRepository heroesRepository) : IProfileService
{
    public async Task<ProfileModel> GetProfile(Guid id)
    {
        var profile = await repository.GetProfile(id) ?? throw new ArgumentNullException("todo provide another decidion later onß");
        profile.Challenges = null;
        profile.Inventory = null;
        profile.MainStatistics = null;
        profile.Clan = null;
        profile.Heroes = null;
        return profile;
    }

    public async Task<ProfileModel> GetProfileDetails(Guid id)
    {
        var profile = await repository.GetProfile(id) ?? throw new ArgumentNullException("todo provide another decidion later onß");
        if (profile.Clan != null) profile.Clan = new ClanModel { Id = profile.Clan.Id, ClanName = "not Implemented yet" };
        return profile;
    }

    public async Task<ProfileModel> GetUserProfile(Guid id, string nickname)
    {
        var profile = await repository.GetProfile(id) ?? await repository.SaveProfile(ProfileModel.DefaultProfile(id, nickname));
        profile.Challenges = null;
        profile.MainStatistics = null;
        profile.Clan = null;
        profile.Heroes = null;
        return profile;
    }

    public async Task<ProfileModel> GetUserProfileDetails(Guid id)
    {
        var profile = await repository.GetProfile(id) ?? throw new ArgumentNullException("todo provide another decidion later onß");
        if (profile.Clan != null) profile.Clan = new ClanModel { Id = profile.Clan.Id, ClanName = "not Implemented yet" };
        try
        {
            await heroesRepository.GetHeroes(profile.Heroes!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return profile;
    }
}
