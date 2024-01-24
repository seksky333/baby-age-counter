using System.ComponentModel.DataAnnotations;

namespace BabyAgeCounter.Server.models;

public class Baby
{
    [Key]
    public Guid? Id { get; set; }
    public DateTime Age { get; set; }
    public DateTime DueDate { get; set; }
}