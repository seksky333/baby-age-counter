using BabyAgeCounter.Server;
using BabyAgeCounter.Server.data;
using BabyAgeCounter.Server.Filter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AzureDB") ??
                       throw new ArgumentNullException($"DB connection string is not found");
var userServiceUrl = builder.Configuration.GetSection("BaseUrls")["UserService"] ??
                     throw new ArgumentNullException($"UserService its BaseUrl is not found");

Console.WriteLine($"userServiceUrl ${userServiceUrl}");


new DiService().ConfigureServices(services: builder.Services,
    connectionString: connectionString,
    userServiceUrl: userServiceUrl);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Authorization Header",
        Version = "v1"
    });
    options.OperationFilter<AuthorizationHeaderFilter>();
});


var allowedOrigins = "localhost";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors(allowedOrigins);

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();