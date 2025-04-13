using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

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
        var entry = context.Reviews.Add(review);
        movie?.Reviews.Add(review);
        context.SaveChanges();
        return entry.Entity;
    }
}