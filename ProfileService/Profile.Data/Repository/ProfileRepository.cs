using Profile.Domain.Repository;

namespace Profile.Data.Repository;

internal class ProfileRepository : IProfileRepository
{
    public async Task<string> GetProfile(Guid id)
    {
        return await Task<string>.Factory.StartNew(() => "4atg89");
    }
}