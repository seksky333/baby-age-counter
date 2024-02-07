using BabyAgeCounter.Server.data;
using BabyAgeCounter.Server.models;
using Microsoft.EntityFrameworkCore;

namespace BabyAgeCounter.Server.Repositories;

public class BabyRepository : IRepository<BabyEntity>, IDisposable
{
    private BabyContext _dbContext;
    public IEnumerable<BabyEntity> GetAll()
    {
        return _dbContext.Baby.ToList();
    }

    public BabyEntity GetById(int id)
    {
        return _dbContext.Baby.Find(id);
    }

    public void Insert(BabyEntity entity)
    {
        _dbContext.Baby.Add(entity);
    }


    public void Update(BabyEntity updatedEntity, int id)
    {
        _dbContext.Entry(updatedEntity).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        BabyEntity baby = _dbContext.Baby.Find(id);
        _dbContext.Baby.Remove(baby);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    private bool disposed = false;

    public BabyRepository(BabyContext dbContext)
    {
        this._dbContext = dbContext;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}