using Account;
using Account.Authentication;
using Account.Data;
using Account.Data.Repository;
using Account.Services;
using JwtLibrary;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IEncryptor, Encryptor>();
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

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

app.MapPost("/login", (LoginRequest request, ITokenGenerator generator) =>
{
    return new
    {
        access_token = generator.GenerateAccessToken(Guid.NewGuid().ToString(), request.Email, Guid.NewGuid())
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
