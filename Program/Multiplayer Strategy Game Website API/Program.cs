using Microsoft.EntityFrameworkCore;
using Multiplayer_Strategy_Game_Website_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthorization();

// Add EF Core DI
builder.Services.AddDbContext<GameSiteDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("GameSiteDBContext")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
