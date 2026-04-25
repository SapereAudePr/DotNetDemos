using Api.Requests.TPH;
using Domain.Entities.TPH;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Endpoints.TPT;

public static class HospitalTptEndpoints
{
    public static IEndpointRouteBuilder MapHospitalTptRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tpt/hospitals")
            .WithTags("TPT - Hospitals");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:int}", Update);
        group.MapDelete("/{id:int}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(HospitalTphDbContext db)
    {
        var hospitals = await db.Hospitals
            .AsNoTracking()
            .ToListAsync();

        return Results.Ok(hospitals);
    }

    private static async Task<IResult> GetById(int id, HospitalTphDbContext db)
    {
        var hospital = await db.Hospitals.FindAsync(id);

        return hospital is null
            ? Results.NotFound()
            : Results.Ok(hospital);
    }

    private static async Task<IResult> Create(CreateHospitalRequest request, HospitalTphDbContext db)
    {
        var hospital = new Hospital(
            request.Address,
            new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label),
            new EmailAddress(request.EmailAddress.Value),
            request.BuiltDate
        )
        {
            Name = request.Name,
            CreatedBy = "Admin-Test",
        };

        await db.Hospitals.AddAsync(hospital);
        await db.SaveChangesAsync();

        return Results.Created($"/tpt/hospitals/{hospital.Id}", hospital);
    }

    private static async Task<IResult> Update(int id, UpdateHospitalRequest request, HospitalTphDbContext db)
    {
        var hospital = await db.Hospitals.FindAsync(id);

        if (hospital is null)
            return Results.NotFound();

        hospital.Name = request.Name;
        hospital.UpdateAddress(request.Address);
        hospital.UpdatePhoneNumber(new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label));
        hospital.UpdateEmailAddress(new EmailAddress(request.EmailAddress.Value));
        hospital.UpdateBuiltDate(request.BuiltDate);
        hospital.UpdatedBy = "Admin-Test";

        await db.SaveChangesAsync();

        return Results.Ok(hospital);
    }

    private static async Task<IResult> Delete(int id, HospitalTphDbContext db)
    {
        var hospital = await db.Hospitals.FindAsync(id);

        if (hospital is null)
            return Results.NotFound();

        db.Hospitals.Remove(hospital);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
}