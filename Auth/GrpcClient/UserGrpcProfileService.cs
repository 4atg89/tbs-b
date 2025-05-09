using ProfileContracts.Profile;

namespace Auth.GrpcClient;

public class UserGrpcProfileService: IUserGrpcProfileService
{

    private readonly ProfileService.ProfileServiceClient _profileClient;

    public UserGrpcProfileService(ProfileService.ProfileServiceClient profileClient)
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