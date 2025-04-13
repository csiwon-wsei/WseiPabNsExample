using ApplicationCore.Domain.Exceptions;
using ApplicationCore.Domain.Models;

namespace ApplicationCore.Application.Services;

public interface IMovieService
{
    public IEnumerable<Movie> GetMovies();

    public Task<IQueryable<Movie>> GetMoviesAsync();

    public Movie? GetById(Guid id);

    public Review AddReviewMovie(Review review);
}