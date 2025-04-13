using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Exceptions;
using ApplicationCore.Domain.Models;

namespace ApplicationCore.Application.Services;

public class MovieService: IMovieService
{
    private IGenericRepository<Movie> _movies;
    private IGenericRepository<User> _users;
    private IGenericRepository<Review> _reviews;

    public MovieService(IGenericRepository<User> users, IGenericRepository<Movie> movies, IGenericRepository<Review> reviews)
    {
        _users = users;
        _movies = movies;
        _reviews = reviews;
    }

    public IEnumerable<Movie> GetMovies()
    {
        return _movies.GetAll();
    }
    
    public async Task<IQueryable<Movie>> GetMoviesAsync()
    {
        await Task.Delay(1);
        return _movies.GetAll();
    }

    public Movie? GetById(Guid id)
    {
        return _movies.GetById(id);
    }

    public Review AddReviewMovie(Review review)
    {
        var movie = _movies.GetById(review.MovieId);
        var user = _users.GetById(review.UserId);
        review.Id = Guid.NewGuid(); 
        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie not found with id: {review.MovieId}!");
        }
        if (user == null)
        {
            throw new UserNotFoundException($"User not found with id: {review.UserId}!");
        }

        _reviews.Add(review);
        movie.Reviews.Add(review);
        _movies.Update(movie);
        _movies.SaveChanges();        return review;
    }
}