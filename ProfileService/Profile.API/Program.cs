using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Profile.API.Apis;
using Profile.Domain;
using Profile.Infrastructure;
using Profile.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// dotnet ef database update -p Profile.Infrastructure -s Profile.API      
// dotnet ef migrations add InitialMigration -p Profile.Infrastructure -s Profile.API
builder.Services.AddDbContextFactory<ProfileDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProfileServices();
builder.Services.AddProfileRepository();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    // options.ReportApiVersions = true; 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapProfileApi();
app.Run();
