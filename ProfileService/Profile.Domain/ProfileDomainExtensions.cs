using Microsoft.Extensions.DependencyInjection;

namespace Profile.Domain;

public static class ProfileDomainExtensions
{
    public static IServiceCollection AddProfileServices(this IServiceCollection services)
    {
        services.AddSingleton<IProfileService, ProfileService>();
        return services;
    }
}