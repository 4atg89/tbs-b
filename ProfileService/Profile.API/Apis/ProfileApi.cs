using Microsoft.AspNetCore.Http.HttpResults;
using Profile.API.Model;
using Profile.Domain;

namespace Profile.API.Apis;

public static class ProfileApi
{
    public static IEndpointRouteBuilder MapProfileApi(this IEndpointRouteBuilder app)
    {

        var vApi = app.NewVersionedApi("Profile");
        var api = vApi.MapGroup("api/profile");

        api.MapGet("/", GetProfile);

        return app;
    }

    public static async Task<Ok<ProfileResponse>> GetProfile(IProfileService service)
    {

        return TypedResults.Ok(new ProfileResponse
        {
            Id = Guid.NewGuid(),
            Nickname = await service.GetUserProfile(),
            Rating = 0,
            Inventory = new InventoryResponse
            {
                Coins = 10,
                Gems = 20
            }
        });
    }

}