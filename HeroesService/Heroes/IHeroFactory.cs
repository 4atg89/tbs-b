using HeroesService.Grpc;

namespace HeroesService.Heroes;

internal interface IHeroFactory
{
    public HeroResponseDto BuildHero(int level, int id);
}

// pogs - фишки 
// inserts, collectible cards, Gumsters - вкладыши 
// stickers - наклейки