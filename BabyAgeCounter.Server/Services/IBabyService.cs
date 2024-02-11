using BabyAgeCounter.Server.models;

namespace BabyAgeCounter.Server.Services;

public interface IBabyService
{
    Task<List<BabyDto>> FindAll();
    Task<BabyEntity?> FindById(Guid id);
    Task AddBaby(BabyEntity entity);
    Task UpdateBaby(BabyEntity updatedEntity);
    Task RemoveBaby(Guid id);
}