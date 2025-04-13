using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Exceptions;
using ApplicationCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class EfMovieService(AppDbContext context): IMovieService
{
    public IEnumerable<Movie> GetMovies()
    {
        return context.Movies;
    }

    public Task<IQueryable<Movie>> GetMoviesAsync()
    {
        return Task.FromResult<IQueryable<Movie>>(context.Movies);
    }

    public Movie? GetById(Guid id)
    {
        return context.Movies.Include(m => m.Reviews).FirstOrDefault(m => m.Id == id);
    }

    public Review AddReviewMovie(Review review)
    {
        var movie = context.Movies.Find(review.MovieId);
        var user = context.Movies.Find(review.UserId);
        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie not found with id: {review.MovieId}!");
        }
        if (user == null)
        {
            throw new UserNotFoundException($"User not found with id: {review.UserId}!");
        }
        context.Reviews.Add(review);
        movie.Reviews.Add(review);
        context.Movies.Update(movie);
        context.SaveChanges();        
        return review;
    }
}