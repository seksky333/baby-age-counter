using BabyAgeCounter.Server.data;
using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.utilities;
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
        var babyEntityList = await _dbContext.Baby.ToListAsync();
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

        await _dbContext.AddAsync(newBaby);
        await _dbContext.SaveChangesAsync();
        return Ok(newBaby);
    }

    [HttpPut("Baby")]
    public async Task<IActionResult> UpdateBaby([FromBody] BabyEntity updatedBaby, Guid id)
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


        _dbContext.Baby.Remove(new BabyEntity
        {
            Id = id
        });
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}