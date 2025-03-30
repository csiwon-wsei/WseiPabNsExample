using System.Net.Mime;
using ApplicationCore.Application.Repository;
using ApplicationCore.Application.Services;
using ApplicationCore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers;
[ApiController]
[Route("/api/v1/admin/movies")]
public class AdminMoviesController(IGenericRepository<Movie> repository): ControllerBase
{
    [HttpGet]
    public async IAsyncEnumerable<Movie> GetAll()
    {
        var movies = repository.GetAll();
        foreach (var movie in movies)
        {
            yield return  movie;
        }
    }
    
    [HttpPatch("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.JsonPatch)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [HttpGet("{id:guid}")]
    public IActionResult GetMovie(Guid id)
    {
        return Ok(repository.GetById(id));
    }

    [HttpPost]
    public IActionResult PostMovie(NewMovieDto dto)
    {
        var movie = new Movie()
        {
            Title = dto.Title,
            Description = dto.Description
        };
        repository.Add(movie);
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteMovie(Guid id)
    {
        var deleteById = repository.DeleteById(id);
        return deleteById ? NoContent() : NotFound();
    }

    [HttpPut("{id:guid}")]
    public IActionResult PutMovie(Guid id, Movie movie)
    {
        var updatedMovie = repository.GetById(id);
        if (updatedMovie == null)
        {
            return NotFound();
        }
        if (id != movie.Id)
        {
            return BadRequest();
        }
        repository.Update(movie);
        return Ok(movie);
    }
}