using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Models;

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

var app = builder.Build();

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
