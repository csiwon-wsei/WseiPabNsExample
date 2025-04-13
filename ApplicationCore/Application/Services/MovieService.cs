using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Exceptions;
using ApplicationCore.Domain.Models;

namespace ApplicationCore.Application.Services;

public class MovieService(IGenericRepository<User> users, IGenericRepository<Movie> movies, IGenericRepository<Review> reviews): IMovieService
{

    public IEnumerable<Movie> GetMovies()
    {
        return movies.GetAll();
    }
    
    public async Task<IQueryable<Movie>> GetMoviesAsync()
    {
        await Task.Delay(1);
        return movies.GetAll();
    }

    public Movie? GetById(Guid id)
    {
        return movies.GetById(id);
    }

    public Review AddReviewMovie(Review review)
    {
        var movie = movies.GetById(review.MovieId);
        var user = users.GetById(review.UserId);
        review.Id = Guid.NewGuid(); 
        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie not found with id: {review.MovieId}!");
        }
        if (user == null)
        {
            throw new UserNotFoundException($"User not found with id: {review.UserId}!");
        }

        reviews.Add(review);
        movie.Reviews.Add(review);
        movies.Update(movie);
        return review;
    }
}