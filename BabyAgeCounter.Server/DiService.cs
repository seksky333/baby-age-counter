using BabyAgeCounter.Server.data;
using BabyAgeCounter.Server.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BabyAgeCounter.Server;

public class DiService
{
    public void ConfigureServices(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<BabyContext>(options =>
            options.UseCosmos(connectionString, "BabyDB")
        );
        services.AddScoped<IBabyRepository, BabyRepository>();
        services.AddControllers();
    }
}