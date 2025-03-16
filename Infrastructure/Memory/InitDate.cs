using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObjects;

namespace Infrastructure.Memory;

public class InitData
{
    public static void Init(IGenericRepository<Movie> moviesRepo, IGenericRepository<User> usersRepo)
    {
        var u1 = new User() { Id = Guid.Parse("6049fb69-f573-4088-a1c5-4e6189f2f135"), Username = "user1" };
        var u2 = new User() { Id = Guid.Parse("D63213CF-9E7B-470F-A7F8-CE996DB31D06"), Username = "user2" };
        usersRepo.Add(u1);
        usersRepo.Add(u2);

        Movie m1 = new Movie()
        {
            Title = "Tenet",
            Descritpion = "Sci-fi movie",
        };
        m1.Id = Guid.Parse("4A30CD68-5AC9-4782-B537-D8A0DF77E809");
        m1.Reviews = new List<Review>()
        {
            new()
            {
                Id = m1.Id,
                UserId = u1.Id,
                MovieId = m1.Id,
                Title = "Test 1",
                Content = "Content 1",
                Rate = Rate.Of(5)
            },
            new()
            {
                Id = m1.Id,
                UserId = u2.Id,
                MovieId = m1.Id,
                Title = "Test 2",
                Content = "Content 2",
                Rate = Rate.Of(9)
            },
        };
        moviesRepo.Add(m1);
    }
}