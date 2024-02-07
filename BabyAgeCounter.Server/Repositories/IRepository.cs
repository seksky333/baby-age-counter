using BabyAgeCounter.Server.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BabyAgeCounter.Server.Repositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Insert(T entity);
    void Update(T updatedEntity, int id);
    void Delete(int id);
    void Save();
}