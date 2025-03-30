using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;
using Infrastructure.Memory;

namespace UnitTests;

public class GenericRepositoryTest
{
    [Fact]
    public void ShouldReturnOneMovieAfterAddingMovieToEmptyRepository()
    {
        // Arrange
        IGenericRepository<Movie> repository = new MemoryGenericRepository<Movie>();
        
        // Act
        repository.Add(new Movie()
        {
            Title = "Test",
            Description = "Test description",
        });
        
        // Assert
        Assert.Equal(1, repository.GetAll().Count());
        Assert.Contains(repository.GetAll(), e => e.Title == "Test" && e.Description == "Test description");
    }

    [Fact]
    public void ShouldRepositoryBeEmptyAfterDeleteMovie()
    {
        // Arrange
        IGenericRepository<Movie> repository = new MemoryGenericRepository<Movie>();
        var movie = new Movie()
        {
            Title = "Test",
            Description = "Test description",
        };
        movie = repository.Add(movie);
        
        // Act
        repository.DeleteById(movie.Id);
        
        //Assert
        Assert.Equal(0, repository.GetAll().Count());
    }
}