using Grpc.Core;
using HeroesService.Grpc;
using HeroesService.Heroes;

namespace HeroesService.GRPC;

public class HeroesGRPCService : HeroService.HeroServiceBase
{

    private IHeroFactory factory = new HeroFactory();
    public override Task<HeroesResponse> GetHeroes(HeroesRequest request, ServerCallContext context)
    {
        foreach (var item in request.Heroes)
        {
            Console.WriteLine($"response => {item.HeroId} {item.Level}");
        }
        return Task.FromResult(new HeroesResponse
        {
            Heroes = { request.Heroes.Select(h => factory.BuildHero(h.Level, h.HeroId)) }
        });
    }

}