namespace BabyAgeCounter.Server.DTOs;

public record BabyDto
{
    public required string Id { get; set; }
    public long Age { get; set; }
    public long DueDate { get; set; }
}