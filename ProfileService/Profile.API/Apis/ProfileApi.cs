using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Extensions;
using Profile.API.Model;
using Profile.Domain;

namespace Profile.API.Apis;

public static class ProfileApi
{
    public static IEndpointRouteBuilder MapProfileApi(this IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Profile");
        var api = vApi.MapGroup("api/profile").RequireAuthorization();

        api.MapGet("/", GetUserProfile);
        api.MapGet("/{id:guid}", GetDetailsById);
        api.MapPatch("/nickname", ChangeNickname);

        return app;
    }

    public static async Task<Results<Ok, NotFound, BadRequest<ProblemDetails>>> ChangeNickname(HttpContext context, IProfileService service, [FromBody] ChangeNicknameRequest request)
    {
        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return TypedResults.NotFound();

        var success = await service.ChangeUserNickname(Guid.Parse(userIdClaim), request.NewNickname);
        if (!success) return TypedResults.NotFound();

        return TypedResults.Ok();
    }

    public static async Task<Results<Ok<ProfileResponse>, NotFound, BadRequest<ProblemDetails>>> GetUserProfile(HttpContext context, IProfileService service)
    {
        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var nickname = context.User?.FindFirst(ClaimTypes.Name)?.Value;

        if (userIdClaim == null || nickname == null) return TypedResults.NotFound();

        var profile = await service.GetUserProfile(Guid.Parse(userIdClaim), nickname);
        if (profile == null) return TypedResults.BadRequest<ProblemDetails>(new() { Detail = "User does not exist" });

        return TypedResults.Ok(profile.MapUserProfileDetails());
    }

    public static async Task<Results<Ok<ProfileResponse>, NotFound, BadRequest<ProblemDetails>>> GetDetailsById(IProfileService service, Guid id)
    {
        var profile = await service.GetAnotherUserProfile(id);
        if (profile == null) return TypedResults.NotFound();

        return TypedResults.Ok(profile.MapProfileDetails());
    }
}