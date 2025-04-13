using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;

namespace Infrastructure.EF;

public class EfReviewRepository(AppDbContext context) : IGenericRepository<Review>
{
    public IQueryable<Review> GetAll()
    {
        return context.Reviews;
    }

    public Review? GetById(Guid id)
    {
        return context.Reviews.Find(id);
    }

    public Review Add(Review entity)
    {
        var entry = context.Reviews.Add(entity);
        context.SaveChanges();
        return entry.Entity;
    }

    public Review Update(Review entity)
    {
        var entry = context.Reviews.Update(entity);
        context.SaveChanges();
        return entry.Entity;
    }

    public bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}