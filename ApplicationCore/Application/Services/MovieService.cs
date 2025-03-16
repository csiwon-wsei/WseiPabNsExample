using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Exceptions;
using ApplicationCore.Domain.Models;

namespace ApplicationCore.Application.Services;

public class MovieService
{
    private IGenericRepository<Movie> _movies;
    private IGenericRepository<User> _users;

    public MovieService(IGenericRepository<User> users, IGenericRepository<Movie> movies)
    {
        _users = users;
        _movies = movies;
    }

    public IEnumerable<Movie> GetMovies()
    {
        return _movies.GetAll();
    }

    public Movie? GetById(Guid id)
    {
        return _movies.GetById(id);
    }

    public Movie AddReviewMovie(Review review)
    {
        var movie = _movies.GetById(review.MovieId);
        var user = _users.GetById(review.UserId);
        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie not found with id: {review.MovieId}!");
        }
        if (user == null)
        {
            throw new UserNotFoundException($"User not found with id: {review.UserId}!");
        }
        movie.Reviews.Add(review);
        return _movies.Update(movie);
    }
}