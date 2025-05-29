namespace Heroes.Apis;

public static class HeroesApi
{

    public static IEndpointRouteBuilder MapHeroesApi(this IEndpointRouteBuilder app)
    {
        var vApi = app.NewVersionedApi("Heroes");
        var api = vApi.MapGroup("api/heroes").RequireAuthorization();
        return app;
    }
    
    
}