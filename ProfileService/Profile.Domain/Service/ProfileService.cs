using Profile.Domain.Model;
using Profile.Domain.Repository;

namespace Profile.Domain;

internal class ProfileService(IProfileRepository repository, IHeroService heroService) : IProfileService
{

    public async Task<ProfileModel> GetAnotherUserProfile(Guid id)
    {
        var profile = await repository.GetProfile(id) ?? throw new ArgumentNullException("Profile not found");
        if (profile.Clan != null) profile.Clan = new ClanModel { Id = profile.Clan.Id, ClanName = "not Implemented yet" };
        return profile;
    }

    public async Task<ProfileModel> GetUserProfile(Guid id, string nickname)
    {
        var profile = await repository.GetProfile(id) ?? await repository.SaveProfile(ProfileModel.DefaultProfile(id, nickname));
        if (profile.Clan != null) profile.Clan = new ClanModel { Id = profile.Clan.Id, ClanName = "not Implemented yet" };
        profile.Heroes = await heroService.GetHeroesDetails(profile.Heroes!);
        return profile;
    }

    public async Task<bool> ChangeUserNickname(Guid userId, string newNickname)
    {
        var result = await repository.ChangeNickname(userId, newNickname);
        return result;
    }
}
