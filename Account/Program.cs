using Account.Authentication;
using Account.Data;
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
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

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

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)],
            users.Count
        ))
        .ToArray();
    return forecast;
})
.RequireAuthorization()
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary, int Count)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
