using Api.Endpoints.TPH;
using Api.Endpoints.TPT;
using Api.ExceptionHandling;
using Application.Repositories.TPT;
using Application.Services.TPT;
using Infrastructure;
using Infrastructure.Repositories.TPT;
using Scalar.AspNetCore;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddExceptionHandler<AppExceptionHandler>();
        builder.Services.AddExceptionHandler<FallbackExceptionHandler>();

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddProblemDetails();

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddScoped<IDepartmentTptRepository, DepartmentTptRepository>();
        builder.Services.AddScoped<IDepartmentTptService, DepartmentTptService>();

        var app = builder.Build();

        app.UseExceptionHandler();

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