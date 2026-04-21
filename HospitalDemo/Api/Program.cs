using System.Net.Mime;
using Domain.Entities.TPT;
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

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<HospitalTphDbContext>();

            var hospital = new Hospital(
                address: "123 Main Street",
                mainPhoneNumber: new PhoneNumber("5551234567", "MainNumber"),
                mainEmailAddress: new EmailAddress("info@hospital.com"),
                builtDate: DateTimeOffset.UtcNow
            );

            context.Add(hospital);
            context.SaveChanges();
        }

        Console.ReadKey();
        
        app.Run();
    }
}