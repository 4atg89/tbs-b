using Microsoft.Extensions.DependencyInjection;
using Profile.Data.Repository;
using Profile.Domain.Repository;

namespace Profile.Data;

public static class ProfileRepositoryExtension
{
    public static IServiceCollection AddProfileRepository(this IServiceCollection services)
    {
        services.AddSingleton<IProfileRepository, ProfileRepository>();
        return services;
    }
}
