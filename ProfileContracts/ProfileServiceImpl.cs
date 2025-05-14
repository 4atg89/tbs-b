using Grpc.Core;
using ProfileContracts.Profile;

namespace Profile.GrpcService;

// this should be passed where server is as grpc works only for https or http2
// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenLocalhost(5031, listenOptions =>
//     {
//         listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
//     });
// });
public class ProfileServiceImpl : ProfileService.ProfileServiceBase
{
    public override Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
    {
        Console.WriteLine($"ProfileContracts Profile -> {request.Nickname} and {request.UserId}");
        return Task.FromResult(new CreateProfileResponse { Success = true });
    }
}