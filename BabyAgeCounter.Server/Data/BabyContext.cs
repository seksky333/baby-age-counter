using BabyAgeCounter.Server.models;
using Microsoft.EntityFrameworkCore;

namespace BabyAgeCounter.Server.data;

public class BabyContext : DbContext
{
    public BabyContext(DbContextOptions options) : base(options)
    {
    }
    public virtual DbSet<BabyEntity> Baby { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultContainer("BabyDBContainer");
        builder.Entity<BabyEntity>()
            .ToContainer(nameof(Baby))
            .HasPartitionKey(b => b.Id)
            .HasNoDiscriminator();
    }
}