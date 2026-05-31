using Api.Endpoints.TPH;
using Api.Endpoints.TPT;
using Application.Repositories.TPT;
using Infrastructure;
using Infrastructure.Repositories.TPT;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;

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

        builder.Services.AddScoped<IDepartmentTptRepository, DepartmentTptRepository>();

        var app = builder.Build();

        
        //http://localhost:5080/scalar/
        app.MapOpenApi();
        app.MapScalarApiReference();

        app.MapTphHospitalRoutes();
        app.MapDepartmentTphRoutes();
        app.MapDepartmentTptRoutes();
        app.MapHospitalTptRoutes();

        app.Run();
    }
}