using BabyAgeCounter.Server.data;
using BabyAgeCounter.Server.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabyAgeCounter.Server.Controllers;

[ApiController]
[Route("/")]
public class BabyController : ControllerBase
{
    private readonly BabyContext _dbContext;
    private static bool _ensureCreated { get; set; } = false;

    public BabyController(BabyContext dbContext)
    {
        _dbContext = dbContext;

        if (!_ensureCreated)
        {
            _dbContext.Database.EnsureCreated();
            _ensureCreated = true;
        }
    }

    [HttpGet("Baby")]
    public async Task<IActionResult> GetBaby()
    {
        return Ok(await _dbContext.Baby.ToListAsync());
    }

    [HttpPost("Baby")]
    public async Task<IActionResult> CreateBaby([FromBody] Baby baby)
    {
        var newBaby = new Baby
        {
            Age = baby.Age,
            DueDate = baby.DueDate
        };

        await _dbContext.AddAsync(newBaby);
        await _dbContext.SaveChangesAsync();
        return Ok(newBaby);
    }

    [HttpPut("Baby")]
    public async Task<IActionResult> UpdateBaby([FromBody] Baby updatedBaby, Guid id)
    {
        var existingBaby = await _dbContext.Baby.FirstOrDefaultAsync(baby => baby.Id == id);
        if (existingBaby is null)
        {
            return NotFound($"Given baby:${id} cannot be found!");
        }

        existingBaby.Age = updatedBaby.Age;
        existingBaby.DueDate = updatedBaby.DueDate;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("Baby")]
    public async Task<IActionResult> RemoveBaby(Guid id)
    {
        var existingBaby = await _dbContext.Baby.FirstOrDefaultAsync(baby => baby.Id == id);
        if (existingBaby is null)
        {
            return NotFound($"Given baby:${id} cannot be found!");
        }


        _dbContext.Baby.Remove(new Baby
        {
            Id = id
        });
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}