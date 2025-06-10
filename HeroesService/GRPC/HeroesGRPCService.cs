using Grpc.Core;
using HeroesService.Grpc;
using HeroesService.Heroes;

namespace HeroesService.GRPC;

public class HeroesGRPCService : HeroService.HeroServiceBase
{

    private IHeroFactory factory = new HeroFactory();
    public override Task<HeroesResponse> GetHeroes(HeroesRequest request, ServerCallContext context)
    {
        var heroMap = request.Heroes.ToDictionary(h => h.HeroId, h => h.Level);

        return Task.FromResult(new HeroesResponse
        {
            Heroes = { Enumerable.Range(1, HeroFactory.MaxId)
                                 .Select(i =>
                                    {
                                        heroMap.TryGetValue(i, out var level);
                                        return factory.BuildHero(level, i);
                                     })
                    }
        });
    }

}