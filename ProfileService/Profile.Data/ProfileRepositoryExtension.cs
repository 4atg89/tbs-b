using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profile.Data.Data;
using Profile.Data.Repository;
using Profile.Domain;
using Profile.Domain.Repository;
using Grpc.Net.Client;

namespace Profile.Data;

public static class ProfileRepositoryExtension
{
    public static IServiceCollection AddProfile(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddSingleton<ProfileRepository>();

        services.AddSingleton<IProfileRepository>(sp => sp.GetRequiredService<ProfileRepository>());
        services.AddSingleton<IHeroService>(sp => sp.GetRequiredService<ProfileRepository>());

        // dotnet ef database update -p Profile.Data -s Profile.API  
        // dotnet ef migrations add InitialMigration -p Profile.Data -s Profile.API
        services.AddDbContextFactory<ProfileDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        var channel = GrpcChannel.ForAddress("http://tbs-b-heroes-service:80");
        services.AddSingleton(channel);
        
        return services;
    }
}
