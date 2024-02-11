using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.Repositories;
using BabyAgeCounter.Server.utilities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BabyAgeCounter.Server.Services;

public class BabyService(IBabyRepository repoImpl) : IBabyService
{
    public async Task<List<BabyDto>> FindAll()
    {
        var babyEntityList = await repoImpl.FindAll();
        var babyList = babyEntityList.ConvertAll(baby => new BabyDto
        {
            Id = baby.Id.ToString(),
            Age = DateTimeConverter.ToUtcMillis(baby.Age),
            DueDate = DateTimeConverter.ToUtcMillis(baby.DueDate)
        }).ToList();
        
        return babyList;
    }

    public Task<BabyEntity?> FindById(Guid id)
    {
        return repoImpl.FindById(id);
    }

    public async Task AddBaby(BabyEntity entity)
    {
        repoImpl.Add(entity);
        await repoImpl.SaveAsync();
    }

    public async Task UpdateBaby(BabyEntity updatedEntity)
    {
        repoImpl.Update(updatedEntity);
        await repoImpl.SaveAsync();
    }

    public async Task RemoveBaby(Guid id)
    {
        var canRemoved = repoImpl.Remove(id);
        if (canRemoved) await repoImpl.SaveAsync();
        else throw new KeyNotFoundException($"Given baby:${id.ToString()} cannot be found!");
    }
}