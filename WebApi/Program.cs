using ApplicationCore.Application.Repository;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using FluentValidation;
using Infrastructure.EF;
using Infrastructure.Memory;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi;
using WebApi.Handlers;

var builder = WebApplication.CreateBuilder(args);


// Serializer NewtonsoftJson
builder.Services.AddControllers(o =>
    {
        o.ReturnHttpNotAcceptable = true; 
        o.RespectBrowserAcceptHeader = true;      
    })
    //.AddXmlSerializerFormatters()
    .AddNewtonsoftJson();
// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddProblemDetails();
// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlite(builder.Configuration["Connections:SqliteConnection"]);
});

builder.Services.AddTransient<IGenericRepository<Movie>, EfMovieRepository>();
builder.Services.AddTransient<IGenericRepository<Review>, EfReviewRepository>();
builder.Services.AddTransient<IGenericRepository<User>, EfUserRepository>();

builder.Services.AddTransient<IMovieService, EfMovieService>();

var app = builder.Build();
app.UseExceptionHandler();
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