using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profile.Data.Data;
using Profile.Data.Repository;
using Profile.Domain.Repository;

namespace Profile.Data;

public static class ProfileRepositoryExtension
{
    public static IServiceCollection AddProfile(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IProfileRepository, ProfileRepository>();

        // dotnet ef database update -p Profile.Data -s Profile.API  
        // dotnet ef migrations add InitialMigration -p Profile.Data -s Profile.API
        services.AddDbContextFactory<ProfileDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddGrpcClient<HeroesService.Grpc.HeroService.HeroServiceClient>(o =>
        {
            o.Address = new Uri("http://localhost:5032");
        });
        services.AddSingleton<IHeroesGRPCRepository, HeroesGRPCRepository>();
        return services;
    }
}
