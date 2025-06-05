using HeroesService.Grpc;

namespace HeroesService.Heroes;

internal class HeroFactory
{

    private Dictionary<int, IHeroFactory> heroes = new()
    {
        { 1, new BarbarianFactory() },
        { 2, new ArcherFactory() },
        { 3, new MageFactory() },
        { 4, new NinjaFactory() }
    };

    private IHeroFactory GetFactory(int id) => heroes.GetValueOrDefault(id)!;

    public HeroResponseDto BuildHero(int level, int id)
    {
        return GetFactory(id).BuildHero(level);
    }
}