using Grpc.Core;
using HeroesService.Grpc;

namespace HeroesService.GRPC;

public class HeroesGRPCService : HeroService.HeroServiceBase
{
    public override Task<HeroesResponse> GetHeroes(HeroesRequest request, ServerCallContext context)
    {
        var a = request.Heroes.ToList();
        a.ForEach(b => Console.WriteLine($"hero fetched {b.HeroId} {b.Level}"));
        Console.WriteLine($"ProfileContracts Profile -> {request.CalculateSize} ");
        return Task.FromResult(new HeroesResponse { });
    }
}