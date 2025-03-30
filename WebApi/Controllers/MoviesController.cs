using System.Net.Mime;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _service;

        public MoviesController(MovieService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public IEnumerable<Movie> GetAllMovies()
        {
            return _service.GetMovies();
        }

        [HttpGet("{movieId:guid}/reviews/{reviewId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        public ActionResult<Review> GetReviewById(Guid movieId, Guid reviewId)
        {
            var review = _service.GetById(movieId)?.Reviews.FirstOrDefault(r => r.Id == reviewId);
            return review == null ? NotFound() : review;
        }
        
        [HttpGet("{movieId:guid}/reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllReviews(Guid movieId)
        {
            var reviews = _service.GetById(movieId)?.Reviews;
            if (reviews == null)
            {
                return NotFound();
            }
            return Ok(reviews);
        }
        
        [HttpPost("{movieId:guid}/reviews")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [MovieExceptionFilter]  // atrybut przechwyci wyjątek i nie dotzre do gloalnego handler'a 
        public IActionResult AddReview([FromRoute] Guid movieId, [FromBody] ReviewDto dto)
        {
            // użytkownik w późniejszej wersjkji zostanie pobrany z żądania
            var userId = Guid.Parse("D63213CF-9E7B-470F-A7F8-CE996DB31D06");
            Review review = MovieMapper.ReviewTo(dto, movieId: movieId, userId: userId);
            review = _service.AddReviewMovie(review);
            return CreatedAtAction(nameof(GetReviewById), new {movieId = movieId, reviewId = review.Id}, review);
        }
    }
}
