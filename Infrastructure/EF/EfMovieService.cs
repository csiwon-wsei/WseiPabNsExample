using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Exceptions;
using ApplicationCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;
// Alternatywna implementacja serwisu, która nie korzysta z repozytoriów
public class EfMovieService(AppDbContext context): IMovieService
{
    public IEnumerable<Movie> GetMovies()
    {
        return context.Movies.AsEnumerable();
    }

    public Task<IQueryable<Movie>> GetMoviesAsync()
    {
        return Task.FromResult<IQueryable<Movie>>(context.Movies);
    }
    public Movie? GetById(Guid id)
    {
        return context.Movies
            .Include(m => m.Reviews)
            .FirstOrDefault(m => m.Id == id);
    }
    public Review AddReviewMovie(Review review)
    {
        var movie = context.Movies.Find(review.MovieId);
        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie with id = {movie.Id} not found!");
        }

        var user = context.Users.Find(review.UserId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with id = {movie.Id} not found!");
        }
        var entry = context.Reviews.Add(review);
        movie.Reviews.Add(review);
        context.SaveChanges();
        return entry.Entity;
    }
}