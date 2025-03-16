using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;
using Infrastructure.Memory;

namespace WebApi;


    public static class SeedData
    {
        public static void Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var movieRepo = provider.GetService<IGenericRepository<Movie>>();
                var userRepo = provider.GetService<IGenericRepository<User>>();
                if (userRepo is not null && movieRepo is not null)
                {
                    InitData.Init(movieRepo, userRepo);
                }
            }
        }
    }