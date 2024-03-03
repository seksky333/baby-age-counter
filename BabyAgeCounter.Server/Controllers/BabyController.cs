using BabyAgeCounter.Server.DTOs;
using BabyAgeCounter.Server.Filter;
using BabyAgeCounter.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BabyAgeCounter.Server.Controllers;

[ApiController]
[Route("/")]
public class BabyController(IBabyService babyService, IUserService userService) : ControllerBase
{
    [HttpGet("Baby")]
    [TokenAuthorization]
    public async Task<IActionResult> GetBaby()
    {
        var isAuthenticated = await userService.Authenticate("");
        Console.WriteLine($"isAuthenticated: ${isAuthenticated}");

        var babyList = await babyService.FindAll();

        return Ok(babyList);
    }

    [HttpPost("Baby")]
    public async Task<IActionResult> CreateBaby([FromBody] BabyDto baby)
    {
        await babyService.AddBaby(baby);
        return Ok(baby);
    }

    [HttpPut("Baby")]
    public async Task<IActionResult> UpdateBaby([FromBody] BabyDto updatedBaby, Guid id)
    {
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