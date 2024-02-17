using System.ComponentModel.DataAnnotations;

namespace BabyAgeCounter.Server.models;

public record BabyEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Age { get; set; }
    public DateTime DueDate { get; set; }
}

