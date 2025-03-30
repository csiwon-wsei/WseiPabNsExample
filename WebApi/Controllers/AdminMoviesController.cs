using ApplicationCore.Application.Repository;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("/api/admin/movies")]
public class AdminMoviesController(IGenericRepository<Movie> repository): ControllerBase
{
    [HttpPatch("{id:guid}")]
    public IActionResult PatchMovie(Guid id, JsonPatchDocument<Movie> doc)
    {
        var movie = repository.GetById(id);
        if (movie == null)
        {
            return NotFound();
        }
        doc.ApplyTo(movie, ModelState);
        repository.Update(movie);
        return Ok(movie);
    }
}