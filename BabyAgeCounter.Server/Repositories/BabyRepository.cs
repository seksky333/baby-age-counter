using BabyAgeCounter.Server.data;
using BabyAgeCounter.Server.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BabyAgeCounter.Server.Repositories;

public sealed class BabyRepository : IBabyRepository, IDisposable
{
    private static bool EnsureCreated { get; set; } = false;
    private bool _disposed = false;
    private readonly BabyContext _dbContext;

    public BabyRepository(BabyContext dbContext)
    {
        _dbContext = dbContext;

        if (!EnsureCreated)
        {
            _dbContext.Database.EnsureCreated();
            EnsureCreated = true;
        }
    }

    public Task<List<BabyEntity>> FindAll()
    {
        return _dbContext.Baby.ToListAsync();
    }

    public async Task<BabyEntity?> FindById(Guid id)
    {
        return await _dbContext.Baby
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.CompareTo(id) == 0);
    }

    public void Add(BabyEntity entity)
    {
        _dbContext.Baby.Add(entity);
    }


    public void Update(BabyEntity updatedEntity)
    {
        _dbContext.Entry(updatedEntity).State = EntityState.Modified;
    }

    public bool Remove(Guid id)
    {
        BabyEntity? baby = _dbContext.Baby.FirstOrDefault(e => e.Id.Equals(id));
        if (baby == null) return false;
        _dbContext.Baby.Remove(baby);
        return true;
    }

    public Task<int> SaveAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}