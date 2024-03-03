using BabyAgeCounter.Server.data;
using BabyAgeCounter.Server.Mapper;
using BabyAgeCounter.Server.Repositories;
using BabyAgeCounter.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BabyAgeCounter.Server;

public class DiService
{
    public void ConfigureServices(IServiceCollection services, string connectionString,
        string userServiceUrl
    )
    {
        services.AddDbContext<BabyContext>(options =>
            options.UseCosmos(connectionString, "BabyDB")
        );

        services.AddHttpClient("user", client => { client.BaseAddress = new Uri(userServiceUrl); });
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<IBabyRepository, BabyRepository>();
        services.AddScoped<IBabyService, BabyService>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddControllers();
    }
}