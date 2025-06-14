using Profile.Domain.Model;

namespace Profile.Domain;

public interface IProfileService
{
    Task<ProfileModel> GetUserProfile(Guid id, string nickname);
    Task<ProfileModel> GetAnotherUserProfile(Guid id);
    Task<bool> ChangeUserNickname(Guid userId, string newNickname);
}
