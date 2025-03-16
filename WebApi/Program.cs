using ApplicationCore.Application.Repository;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Infrastructure.Memory;
using Scalar.AspNetCore;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IGenericRepository<Movie>, MemoryGenericRepository<Movie>>();
builder.Services.AddSingleton<IGenericRepository<User>, MemoryGenericRepository<User>>();
builder.Services.AddSingleton<MovieService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Seed();
app.Run();