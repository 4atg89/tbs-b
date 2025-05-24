using System.IdentityModel.Tokens.Jwt;
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
        var api = vApi.MapGroup("api/profile").RequireAuthorization(); ;

        api.MapGet("/", GetUserProfile);
        api.MapGet("/details", GetUserProfileDetails);

        api.MapGet("/{id:guid}", GetProfileById);
        api.MapGet("/{id:guid}/details", GetDetailsById);

        api.MapPatch("/", ChangeUserName);

        // change active hands

        return app;
    }

    public static async Task<Results<Ok<ProfileResponse>, NotFound, BadRequest<ProblemDetails>>> GetUserProfile(HttpContext context, IProfileService service)
    {
        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var nickname = context.User?.FindFirst(ClaimTypes.Name)?.Value;

        if (userIdClaim == null || nickname == null) return TypedResults.NotFound();

        var profile = await service.GetUserProfile(Guid.Parse(userIdClaim), nickname);
        if (profile == null) return TypedResults.BadRequest<ProblemDetails>(new() { Detail = "User not created" });

        return TypedResults.Ok(profile.MapUserProfile());
    }

    public static async Task<Results<Ok<ProfileResponse>, NotFound, BadRequest<ProblemDetails>>> GetUserProfileDetails(HttpContext context, IProfileService service)
    {
        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return TypedResults.NotFound();

        var profile = await service.GetUserProfileDetails(Guid.Parse(userIdClaim));
        if (profile == null) return TypedResults.NotFound();

        return TypedResults.Ok(profile.MapUserProfileDetails());
    }

    public static async Task<Results<Ok<ProfileResponse>, NotFound, BadRequest<ProblemDetails>>> GetProfileById(IProfileService service, Guid id)
    {
        var profile = await service.GetProfile(id);
        if (profile == null) return TypedResults.NotFound();

        return TypedResults.Ok(profile.MapProfile());
    }

    public static async Task<Results<Ok<ProfileResponse>, NotFound, BadRequest<ProblemDetails>>> GetDetailsById(IProfileService service, Guid id)
    {
        var profile = await service.GetProfileDetails(id);
        if (profile == null) return TypedResults.NotFound();

        return TypedResults.Ok(profile.MapProfileDetails());
    }

    public static async Task<Results<NotFound, BadRequest<ProblemDetails>>> ChangeUserName(IProfileService service)
    {
        return TypedResults.NotFound();
    }

}