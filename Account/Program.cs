using Account;
using Account.Authentication;
using Account.Data;
using Account.Data.Repository;
using Account.Services;
using JwtLibrary;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IEncryptor, Encryptor>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
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

app.UseHttpsRedirection();

app.MapPost("/login", async (LoginRequest request, AccountDbContext dbContext) =>
{
    return new
    {
        // access_token = generator.GenerateAccessToken(Guid.NewGuid().ToString(), request.Email, Guid.NewGuid())
        ra = await dbContext.Users.ToListAsync()

    };
});
app.MapGet("/weatherforecast", async (HttpContext context, AccountDbContext dbContext) =>
{
    var users = await dbContext.Users.ToListAsync();
    return users;
})
.RequireAuthorization()
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();
