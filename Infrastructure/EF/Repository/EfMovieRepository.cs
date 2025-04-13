using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository;

public class EfMovieRepository(AppDbContext context): IGenericRepository<Movie>
{
    public IQueryable<Movie> GetAll()
    {
        return context.Movies;
    }

    public Movie? GetById(Guid id)
    {
        return context.Movies
            .Include(m => m.Reviews)
            .FirstOrDefault(m => m.Id == id);
    }

    public Movie Add(Movie entity)
    {
        var entry = context.Movies.Add(entity);
        context.SaveChanges();
        return entry.Entity;
    }

    public Movie Update(Movie entity)
    {
        var entry = context.Movies.Update(entity);
        context.SaveChanges();
        return entry.Entity;
    }

    public bool DeleteById(Guid id)
    {
        var deleted = new Movie()
        {
            Id = id
        };
        context.Entry(deleted).State = EntityState.Deleted;
        context.Movies.Remove(deleted);
        try
        {
            context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}