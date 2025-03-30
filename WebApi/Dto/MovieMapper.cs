using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObjects;

namespace WebApi.Dto;

public class MovieMapper
{
    public static Review ReviewTo(ReviewDto dto, Guid movieId, Guid userId)
    {
        return new Review()
        {
            Content = dto.Content,
            Rate = Rate.Of(dto.Rate),
            Title = dto.Title,
            MovieId = movieId,
            UserId = userId,
        };
    }

    public static MovieDto MovieTo(Movie movie)
    {
        return new MovieDto()
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            Reviews = movie.Reviews.Select(r => r.Title).ToList(),
        };
    }
}