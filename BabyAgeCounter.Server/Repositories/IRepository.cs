using BabyAgeCounter.Server.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BabyAgeCounter.Server.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> FindAll();
    Task<T?> FindById(Guid id);
    void Add(T entity);
    void Update(T updatedEntity);
    bool Remove(Guid id);
    Task<int> SaveAsync();
}