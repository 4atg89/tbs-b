using HeroesService.Grpc;
using Microsoft.EntityFrameworkCore;
using Profile.Data.Data;
using Profile.Data.Extensions;
using Profile.Domain;
using Profile.Domain.Model;
using Profile.Domain.Repository;
using Grpc.Net.Client;

namespace Profile.Data.Repository;

internal class ProfileRepository(
    IDbContextFactory<ProfileDbContext> dbContextFactory,
    GrpcChannel grpcChannel) : IProfileRepository, IHeroService
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
            return (await context.Profiles
                .Include(p => p.Heroes)
                .Include(p => p.HandHeroes).FirstOrDefaultAsync(p => p.Id == id)
            )?.MapProfile();
        });

    public async Task<ProfileModel> SaveProfile(ProfileModel profile) => await ExecuteAsync(async context =>
    {
        await context.Profiles.AddAsync(profile.MapProfile());
        await context.SaveChangesAsync();
        return profile;
    });


    public async Task<bool> ChangeNickname(Guid userId, string newNickname) => await ExecuteAsync(async context =>
    {
        var profile = await context.Profiles.FindAsync(userId);
        //todo is not finished need to sync with auth
        profile.Nickname = newNickname;
        await context.SaveChangesAsync();
        return true;
    });

    public async Task<List<HeroesModel>> GetHeroesDetails(List<HeroesModel> heroes)
    {
        var client = new HeroService.HeroServiceClient(grpcChannel);
        var request = new HeroesRequest
        {
            Heroes = { heroes.Select(h => new HeroRequestDto { HeroId = h.HeroId, Level = h.Level }) }
        };

        var response = await client.GetHeroesAsync(request);
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