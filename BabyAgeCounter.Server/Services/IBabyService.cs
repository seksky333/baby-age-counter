using BabyAgeCounter.Server.DTOs;

namespace BabyAgeCounter.Server.Services;

public interface IBabyService
{
    Task<List<BabyDto>> FindAll();
    Task<BabyDto?> FindById(Guid id);
    Task AddBaby(BabyDto newBaby);
    Task UpdateBaby(BabyDto updatedBaby);
    Task RemoveBaby(Guid id);
}