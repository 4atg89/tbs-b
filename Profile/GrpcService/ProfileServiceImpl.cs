using Grpc.Core;
using ProfileContracts.Profile;

namespace Profile.GrpcService;

public class ProfileServiceImpl : ProfileService.ProfileServiceBase
{
    public override Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
    {
        Console.WriteLine($"ProfileContracts Profile -> {request.Nickname} and {request.UserId}");
        return Task.FromResult(new CreateProfileResponse { Success = true });
    }
}