namespace BabyAgeCounter.Server.models;

public record BabyDto
{
    public required string Id { get; set; }
    public long Age { get; set; }
    public long DueDate { get; set; }
}