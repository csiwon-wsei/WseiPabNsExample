using ApplicationCore.Application.Repository;
using ApplicationCore.Domain.Models;

namespace Infrastructure.EF.Repository;

public class EfUSerRepository(AppDbContext context): IGenericRepository<User>
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
        var entry = context.Users.Add(entity);
        context.SaveChanges();
        return entry.Entity;
    }

    public User Update(User entity)
    {
        throw new NotImplementedException();
    }

    public bool DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }
}