using Api.Endpoints.TPH;
using Domain.Entities.TPH;
using Domain.ValueObjects;
using Infrastructure;
using Infrastructure.Persistence;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        app.MapTphHospitalRoutes();

        Console.ReadKey();
        
        app.Run();
    }
}