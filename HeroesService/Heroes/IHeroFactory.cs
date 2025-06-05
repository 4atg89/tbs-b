using HeroesService.Grpc;

namespace HeroesService.Heroes;

internal interface IHeroFactory
{
    public HeroResponseDto BuildHero(int level);
}