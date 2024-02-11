using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.Services;
using BabyAgeCounter.Server.utilities;
using Microsoft.AspNetCore.Mvc;

namespace BabyAgeCounter.Server.Controllers;

[ApiController]
[Route("/")]
public class BabyController(IBabyService babyService) : ControllerBase
{
    [HttpGet("Baby")]
    public async Task<IActionResult> GetBaby()
    {
        var babyList = await babyService.FindAll();
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
        await babyService.AddBaby(newBaby);
        return Ok(newBaby);
    }

    [HttpPut("Baby")]
    public async Task<IActionResult> UpdateBaby([FromBody] BabyEntity updatedBaby, Guid id)
    {
        // var existingBaby = await _dbContext.Baby.FirstOrDefaultAsync(baby => baby.Id == id);
        var existingBaby = await babyService.FindById(id);
        if (existingBaby is null)
        {
            return NotFound($"Given baby:${id} cannot be found!");
        }

        await babyService.UpdateBaby(updatedBaby);
        return NoContent();
    }

    [HttpDelete("Baby")]
    public async Task<IActionResult> RemoveBaby(Guid id)
    {
        await babyService.RemoveBaby(id);
        return Ok();
    }
}