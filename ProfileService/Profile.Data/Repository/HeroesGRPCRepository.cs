using HeroesService.Grpc;
using Profile.Domain.Model;
using Profile.Domain.Repository;

namespace Profile.Data.Repository;

public class HeroesGRPCRepository : IHeroesGRPCRepository
{
    private readonly HeroesService.Grpc.HeroService.HeroServiceClient _heroesClient;

    public HeroesGRPCRepository(HeroService.HeroServiceClient heroesClient)
    {
        _heroesClient = heroesClient;
    }

    public async Task GetHeroes(List<HeroesModel> heroes)
    {

        var request = new HeroesRequest
        {
            Heroes = { heroes.Select(h => new HeroRequestDto { HeroId = h.HeroId, Level = h.Level }) }
        };

        var a = await _heroesClient.GetHeroesAsync(request);
        Console.WriteLine($"response is ${a}");
    }
}