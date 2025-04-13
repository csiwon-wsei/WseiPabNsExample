using ApplicationCore.Application.Commons;

namespace ApplicationCore.Application.Repository;

public interface IGenericRepository<T> where T : BaseIdentity
{
    IQueryable<T> GetAll();

    T? GetById(Guid id);

    T Add(T entity);

    T Update(T entity);
    
    bool DeleteById(Guid id);
    
    void SaveChanges();

}
