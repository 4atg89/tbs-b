using Microsoft.Extensions.DependencyInjection;
using Profile.Domain.Repository;
using Profile.Infrastructure.Repository;

namespace Profile.Infrastructure;

public static class ProfileRepositoryExtension
{
    public static IServiceCollection AddProfileRepository(this IServiceCollection services)
    {
        services.AddSingleton<IProfileRepository, ProfileRepository>();
        return services;
    }
}
