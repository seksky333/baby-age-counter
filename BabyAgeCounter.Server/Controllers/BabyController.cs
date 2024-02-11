using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.Repositories;
using BabyAgeCounter.Server.utilities;
using Microsoft.AspNetCore.Mvc;

namespace BabyAgeCounter.Server.Controllers;

[ApiController]
[Route("/")]
public class BabyController(IBabyRepository repo) : ControllerBase
{
    [HttpGet("Baby")]
    public async Task<IActionResult> GetBaby()
    {
        var babyEntityList = await repo.FindAll();
        var babyList = babyEntityList.ConvertAll(baby => new BabyDto
        {
            Id = baby.Id.ToString(),
            Age = DateTimeConverter.ToUtcMillis(baby.Age),
            DueDate = DateTimeConverter.ToUtcMillis(baby.DueDate)
        }).ToList();
        return Ok(babyList);
    }

    [HttpPost("Baby")]
    public async Task<IActionResult> CreateBaby([FromBody] BabyEntity baby)
    {
        var newBaby = new BabyEntity
        {
            Age = baby.Age,
            DueDate = baby.DueDate
        };
        repo.Add(newBaby);
        await repo.SaveAsync();
        return Ok(newBaby);
    }

    [HttpPut("Baby")]
    public async Task<IActionResult> UpdateBaby([FromBody] BabyEntity updatedBaby, Guid id)
    {
        // var existingBaby = await _dbContext.Baby.FirstOrDefaultAsync(baby => baby.Id == id);
        var existingBaby = await repo.FindById(id);
        if (existingBaby is null)
        {
            return NotFound($"Given baby:${id} cannot be found!");
        }
        repo.Update(updatedBaby);

        existingBaby.Age = updatedBaby.Age;
        existingBaby.DueDate = updatedBaby.DueDate;
        await repo.SaveAsync();
        return NoContent();
    }

    [HttpDelete("Baby")]
    public async Task<IActionResult> RemoveBaby(Guid id)
    {
        var canRemove = repo.Remove(id);

        if (canRemove) await repo.SaveAsync();
        else return NotFound($"Given baby:${id} cannot be found!");

        return Ok();
    }
}