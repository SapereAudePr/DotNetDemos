using Api.Requests;
using Domain.Entities.TPH;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Endpoints.TPH;

public static class HospitalTptEndPoints
{
    public static IEndpointRouteBuilder MapTphHospitalRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tph/hospitals")
            .WithTags("TPH - Hospital");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:int}", Update);
        group.MapDelete("/{id:int}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(HospitalTphDbContext db)
    {
        var hospitals = await db.Hospitals.AsNoTracking().ToListAsync();

        return Results.Ok(hospitals);
    }

    private static async Task<IResult> GetById(int id, HospitalTphDbContext db)
    {
        var hospital = await db.Hospitals.FindAsync(id);

        return hospital is null ? Results.NotFound() : Results.Ok(hospital);
    }

    private static async Task<IResult> Create(CreateHospitalRequest request, HospitalTphDbContext db)
    {
        var phone = new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label);
        var email = new EmailAddress(request.EmailAddress.Value);

        var hospital = new Hospital
        (
            request.Address,
            phone,
            email,
            request.BuiltDate
        );

        hospital.Name = request.Name;
        hospital.CreatedBy = "Admin-Test";
        hospital.UpdatedBy = "Admin-Test";

        await db.Hospitals.AddAsync(hospital);
        await db.SaveChangesAsync();

        return Results.Created($"/tph/hospital/{hospital.Id}", hospital);
    }

    private static async Task<IResult> Update(int id, UpdateHospitalRequest request, HospitalTphDbContext db)
    {
        var hospital = await db.Hospitals.FindAsync(id);
        if (hospital is null)
            return Results.NotFound();

        var phone = new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label);
        var email = new EmailAddress(request.EmailAddress.Value);

        hospital.Name = request.Name;
        hospital.UpdateAddress(request.Address);
        hospital.UpdatePhoneNumber(phone);
        hospital.UpdateEmailAddress(email);
        hospital.UpdateBuiltDate(request.BuiltDate);
        hospital.CreatedBy = "Admin-Test";
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