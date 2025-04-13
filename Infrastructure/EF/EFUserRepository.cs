using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;

namespace Infrastructure.EF;

public class EfUserRepository(AppDbContext context): IGenericRepository<User>
{
    
    public IQueryable<User> GetAll()
    {
        return context.Users;
    }

    public User? GetById(Guid id)
    {
        return context.Users.Find(id);
    }

    public User Add(User entity)
    {
        var e = context.Users.Add(entity);
        context.SaveChanges();
        return e.Entity;
    }

    public User Update(User entity)
    {
        throw new NotImplementedException();
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