using AutoMapper;
using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.Repositories;

namespace BabyAgeCounter.Server.Services;

public class BabyService(IBabyRepository repoImpl, IMapper mapper) : IBabyService
{
    public async Task<List<BabyDto>> FindAll()
    {
        var babyEntityList = await repoImpl.FindAll();
        return mapper.Map<List<BabyDto>>(babyEntityList);
    }

    public async Task<BabyDto?> FindById(Guid id)
    {
        var babyEntity = await repoImpl.FindById(id);
        return mapper.Map<BabyDto>(babyEntity);
    }

    public async Task AddBaby(BabyDto newBaby)
    {
        var babyEntity = mapper.Map<BabyEntity>(newBaby);
        repoImpl.Add(babyEntity);
        await repoImpl.SaveAsync();
    }

    public async Task UpdateBaby(BabyDto updatedBaby)
    {
        var babyEntity = mapper.Map<BabyEntity>(updatedBaby);
        repoImpl.Update(babyEntity);
        await repoImpl.SaveAsync();
    }

    public async Task RemoveBaby(Guid id)
    {
        var canRemoved = repoImpl.Remove(id);
        if (canRemoved) await repoImpl.SaveAsync();
        else throw new KeyNotFoundException($"Given baby:${id.ToString()} cannot be found!");
    }
}