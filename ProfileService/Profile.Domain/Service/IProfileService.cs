using Profile.Domain.Model;

namespace Profile.Domain;

public interface IProfileService
{
    Task<ProfileModel> GetUserProfile(Guid id, string nickname);
    Task<ProfileModel> GetUserProfileDetails(Guid id);
    Task<ProfileModel> GetProfile(Guid id);
    Task<ProfileModel> GetProfileDetails(Guid id);
}
