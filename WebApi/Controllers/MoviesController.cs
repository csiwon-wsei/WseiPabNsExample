using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
