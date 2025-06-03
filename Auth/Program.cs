using Auth;
using Auth.Data;
using Auth.Data.Repository;
using Auth.Services;
using JwtLibrary;
using JwtLibrary.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Shared.RabbitMQ.Extensions;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(5030, listenOptions =>
//     {
//         listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
//     });
// });

builder.Services.AddDbContextFactory<AuthDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis")
        ?? throw new InvalidOperationException("Redis connection string is missing");
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IEncryptor, Encryptor>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<ICodeRepository, CodeRepository>();
builder.Services.AddSingleton<IUserVerificationService, UserVerificationService>();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

// app.UseHttpsRedirection();

app.MapPost("/login", async (LoginRequest request, AuthDbContext dbContext) =>
{
    return new
    {
        // access_token = generator.GenerateAccessToken(Guid.NewGuid().ToString(), request.Email, Guid.NewGuid())
        ra = await dbContext.Users.ToListAsync()

    };
});
app.MapGet("/weatherforecast", async (HttpContext context, AuthDbContext dbContext) =>
{
    var users = await dbContext.Users.ToListAsync();
    return users;
})
.RequireAuthorization()
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();
