using Profile.Domain.Model;

namespace Profile.Domain.Repository;

public interface IProfileRepository
{
    Task<ProfileModel?> GetProfile(Guid id);
    Task<ProfileModel> SaveProfile(ProfileModel profile);
    Task<bool> ChangeNickname(Guid userId, string newNickname);

}