using HeroesService.Grpc;

namespace HeroesService.Heroes;

//todo redo this later when UI is done
internal class HeroFactory : IHeroFactory
{

    private Dictionary<int, IHeroFactory> heroes = new()
    {
        { 1, new BarbarianFactory() },
        { 2, new ArcherFactory() },
        { 3, new MageFactory() },
        { 4, new NinjaFactory() },
        { 5, new KungFuFactory() },
        { 6, new GhostFactory() },
        { 7, new FantomasFactory() },
        { 8, new MaskosFactory() },
        { 9, new GrimFactory() },
        { 10, new TrollFactory() },
    };

    private IHeroFactory GetFactory(int id) => heroes.GetValueOrDefault(id)!;

    public HeroResponseDto BuildHero(int level, int id)
    {
        return GetFactory(id).BuildHero(level, id);
    }
}