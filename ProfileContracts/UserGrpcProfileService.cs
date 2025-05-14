using ProfileContracts.Profile;
using static ProfileContracts.Profile.ProfileService;

namespace ProfileContracts;

// add where required
// builder.Services.AddGrpcClient<ProfileContracts.Profile.ProfileService.ProfileServiceClient>(o =>
// {
//     o.Address = new Uri("http://localhost:5031");
// });
public class UserGrpcProfileService : IUserGrpcProfileService
{
private readonly ProfileServiceClient _profileClient;

    public UserGrpcProfileService(ProfileServiceClient profileClient)
    {
        _profileClient = profileClient;
    }

    public async Task RegisterUserAsync(string userId, string nickname)
    {
        var response = await _profileClient.CreateProfileAsync(new CreateProfileRequest
        {
            UserId = userId,
            Nickname = nickname
        });

        if (!response.Success)
        {
            throw new Exception("Didn't managed to create a profile");
        }
    }
}