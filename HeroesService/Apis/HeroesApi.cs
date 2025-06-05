using HeroesService.Heroes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HeroesService.Apis;

public static class HeroesApi
{

    public static IEndpointRouteBuilder MapHeroesApi(this IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Heroes");
        var api = vApi.MapGroup("api/heroes");

        api.MapGet("/", (Delegate)GetHeroes);
        return app;
    }


    public static async Task<Results<Ok<BarbarianFactory>, NotFound, BadRequest<ProblemDetails>>> GetHeroes(HttpContext context)
    {
        Console.WriteLine("HeroHeroHero");
        await Task.Delay(500);
        return TypedResults.Ok(new BarbarianFactory());
    }
}