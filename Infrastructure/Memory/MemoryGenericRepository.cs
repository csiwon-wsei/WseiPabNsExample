using ApplicationCore.Application.Commons;
using ApplicationCore.Application.Repository;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T> : IGenericRepository<T> where T : BaseIdentity
{

    private Dictionary<Guid, T> _repo = new Dictionary<Guid, T>();
    public IQueryable<T> GetAll()
    {
        return _repo.Values.AsQueryable();
    }

    public T? GetById(Guid id)
    {
        return _repo.GetValueOrDefault(id);
    }

    public T Add(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        _repo.Add(entity.Id, entity);
        return entity;
    }

    public T Update(T entity)
    {
        if (_repo.ContainsKey(entity.Id))
        {
            _repo[entity.Id] = entity;
        }
        return entity;
    }

    public bool DeleteById(Guid id)
    {
        return _repo.Remove(id);
    }

    public void SaveChanges()
    {
    }
}