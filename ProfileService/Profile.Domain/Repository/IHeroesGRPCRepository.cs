using Profile.Domain.Model;

namespace Profile.Domain.Repository;

public interface IHeroesGRPCRepository
{
    Task GetHeroes(List<HeroesModel> heroes);
}