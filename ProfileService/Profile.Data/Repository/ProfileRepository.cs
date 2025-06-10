using HeroesService.Grpc;
using Microsoft.EntityFrameworkCore;
using Profile.Data.Data;
using Profile.Data.Data.Entities;
using Profile.Data.Extensions;
using Profile.Domain.Model;
using Profile.Domain.Repository;

namespace Profile.Data.Repository;

internal class ProfileRepository(
    IDbContextFactory<ProfileDbContext> dbContextFactory,
    HeroService.HeroServiceClient _heroesClient) : IProfileRepository
{

    private async Task<T> ExecuteAsync<T>(Func<ProfileDbContext, Task<T>> action)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        return await action(context);
    }

    private async Task ExecuteAsync(Func<ProfileDbContext, Task> action)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        await action(context);
    }

    public async Task<ProfileModel?> GetProfile(Guid id) =>
        await ExecuteAsync(async context =>
        {
            var profile = (await context.Profiles
                .Include(p => p.Heroes)
                .Include(p => p.HandHeroes).FirstOrDefaultAsync(p => p.Id == id)
            )?.MapProfile();
            profile!.Heroes = await GetHeroes(profile.Heroes!);
            return profile;
        });

    public async Task<ProfileModel> SaveProfile(ProfileModel profile) => await ExecuteAsync(async context =>
    {
        await context.Profiles.AddAsync(profile.MapProfile());
        await context.SaveChangesAsync();
        return profile;
    });

    private async Task<List<HeroesModel>> GetHeroes(List<HeroesModel> heroes)
    {
        var request = new HeroesRequest
        {
            Heroes = { heroes.Select(h => new HeroRequestDto { HeroId = h.HeroId, Level = h.Level }) }
        };

        var response = await _heroesClient.GetHeroesAsync(request);
        var localDict = heroes.ToDictionary(h => h.HeroId);

        var result = response.Heroes
            .Select(serverHero =>
            {
                if (localDict.TryGetValue(serverHero.HeroId, out var existing))
                {
                    return serverHero.MapHeroesModel(existing);
                }
                else
                {
                    return HeroesModel.DefaultEmptyHero(
                        id: serverHero.HeroId,
                        image: serverHero.Image,
                        name: serverHero.Name,
                        description: serverHero.Description,
                        descriptionTitle: serverHero.DescriptionTitle
                    );
                }
            })
            .ToList();

        return result;
    }

}