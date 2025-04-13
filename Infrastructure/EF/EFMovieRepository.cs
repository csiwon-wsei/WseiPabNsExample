using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public class EfMovieRepository(AppDbContext context) : IGenericRepository<Movie>
{
    public IQueryable<Movie> GetAll()
    {
        return context.Movies.Include(m => m.Reviews);
    }

    public Movie? GetById(Guid id)
    {
        return context.Movies.Include(m => m.Reviews).FirstOrDefault(m => m.Id == id);
    }

    public Movie Add(Movie entity)
    {
        var saved = context.Movies.Add(entity);
        SaveChanges();
        return saved.Entity;
    }

    public Movie Update(Movie entity)
    {
        var updated = context.Movies.Update(entity);
        SaveChanges();
        return updated.Entity;
    }

    public bool DeleteById(Guid id)
    {
        var deleted = new Movie()
        {
            Id = id
        };
        context.Entry(deleted).State = EntityState.Deleted;
        var removed = context.Movies.Remove(deleted);
        try
        {
            SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}