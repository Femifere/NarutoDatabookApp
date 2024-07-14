using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NarutoDatabookApp;
using NarutoDatabookApp.Data;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Repository;
using AutoMapper;
using System.Reflection;
using NarutoDatabookApp.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddScoped<ICharacterInterface, CharacterRepository>();
builder.Services.AddScoped<IFanInterface, FanRepository>(); // Add repositories as per your interfaces
builder.Services.AddScoped<IRankingInterface, RankingRepository>();
builder.Services.AddScoped<ISpecialtyInterface, SpecialtyRepository>();
builder.Services.AddScoped<ITeamInterface, TeamRepository>();
builder.Services.AddScoped<IVillageInterface, VillageRepository>(); // Add Village repository


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Contains("seed"))
{
    Seeder.Run(app.Services);
    return;
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
