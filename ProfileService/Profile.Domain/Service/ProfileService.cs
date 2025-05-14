
using Profile.Domain.Repository;

namespace Profile.Domain;

internal class ProfileService(IProfileRepository repository) : IProfileService
{
    public async Task<string> GetUserProfile()
    {
        return await repository.GetProfile(Guid.NewGuid());
    }
}
