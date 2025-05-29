using Microsoft.EntityFrameworkCore;
using Profile.API.Model;
using Profile.Data.Data;
using Profile.Data.Extensions;
using Profile.Domain.Repository;

namespace Profile.Data.Repository;

internal class ProfileRepository(IDbContextFactory<ProfileDbContext> dbContextFactory) : IProfileRepository
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
        await ExecuteAsync(async context => (await context.Profiles.Include(p => p.Heroes).FirstAsync(p => p.Id == id))?.MapProfile());

    public async Task<ProfileModel> SaveProfile(ProfileModel profile) => await ExecuteAsync(async context =>
    {
        await context.Profiles.AddAsync(profile.MapProfile());
        await context.SaveChangesAsync();
        return profile;
    });
}