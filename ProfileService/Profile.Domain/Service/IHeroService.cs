using Profile.Domain.Model;

namespace Profile.Domain;

public interface IHeroService
{
    Task<List<HeroesModel>> GetHeroesDetails(List<HeroesModel> heroes);
} 