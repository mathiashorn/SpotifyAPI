using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Models;
using SpotifyAPI.Services;
using SpotifyAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Database configuration
var config = builder.Configuration;

var connectionString = config.GetConnectionString("DevelopmentConnection");
builder.Services.AddDbContext<SpotifyDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Database migration
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<SpotifyDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
