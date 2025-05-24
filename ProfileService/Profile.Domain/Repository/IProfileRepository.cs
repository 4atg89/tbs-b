using Profile.API.Model;

namespace Profile.Domain.Repository;

public interface IProfileRepository
{
    Task<ProfileModel?> GetProfile(Guid id);
    Task<ProfileModel> SaveProfile(ProfileModel profile);

}