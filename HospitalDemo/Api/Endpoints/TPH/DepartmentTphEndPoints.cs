using Api.Mappings.TPH;
using Domain.Entities.TPH;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Api.Queries.TPH;
using Api.Requests.TPH;

namespace Api.Endpoints.TPH;

public static class DepartmentTphEndPoints
{
    public static IEndpointRouteBuilder MapDepartmentTphRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tph/departments")
            .WithTags("TPH - Departments");

        group.MapGet("/", GetAll)
            .WithName("TPH.Departments.GetAll")
            .WithSummary("Returns all departments")
            .WithDescription("")
            .Produces<List<Department>>(200);

        group.MapGet("/{id:int}", GetById)
            .WithName("TPH.Departments.GetById")
            .WithSummary("Gets a department by Id")
            .WithDescription("Returns 404 if not found")
            .Produces<Department>(200)
            .Produces(404);

        group.MapPost("/", Create)
            .WithName("TPH.Departments.Create")
            .WithSummary("Creates a new department")
            .WithDescription("")
            .Produces<Department>(201);

        group.MapPatch("/{id:int}", Patch)
            .WithName("TPH.Departments.Patch")
            .WithSummary("Patches a department")
            .WithDescription(
                "If PhoneNumber or Email is included in the request, existing values are replaced with the new value.")
            .Produces<Department>(200)
            .Produces(404);

        group.MapPut("/{id:int}", Update)
            .WithName("TPH.Departments.Update")
            .WithSummary("Updates a department")
            .WithDescription(
                "PhoneNumber and Email's existing values are replaced with the new values.")
            .Produces<Department>(200)
            .Produces(404);

        group.MapDelete("/{id:int}", Delete)
            .WithName("TPH.Departments.Delete")
            .WithSummary("Deletes a department")
            .WithDescription("Returns 404 if not found and 204 if found and deleted")
            .Produces(204)
            .Produces(404);

        return app;
    }

    private static async Task<IResult> GetAll([AsParameters] DepartmentQuery queryParams, HospitalTphDbContext db)
    {
        var query = db.Departments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.FilterOn) && !string.IsNullOrWhiteSpace(queryParams.FilterQuery))
        {
            if (queryParams.FilterOn != null && queryParams.FilterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(x => queryParams.FilterQuery != null && x.Name.Contains(queryParams.FilterQuery));
            }
        }

        if (!string.IsNullOrWhiteSpace(queryParams.SortBy))
        {
            if (queryParams.SortBy != null && queryParams.SortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                query = queryParams.SortAscending ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
            }
        }

        var skip = (queryParams.PageNumber - 1) * queryParams.PageSize;

        var departments = await
            query.Skip(skip)
                .Take(queryParams.PageSize)
                .AsNoTracking()
                .ToListAsync();

        return Results.Ok(departments.ToResponse());
    }

    private static async Task<IResult> GetById(int id, HospitalTphDbContext db)
    {
        var department = await db.Departments.FindAsync(id);

        return department is null ? Results.NotFound() : Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Create(CreateDepartmentRequest request, HospitalTphDbContext db)
    {
        var phone = new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label);
        var email = new EmailAddress(request.Email.Value);

        var department = new Department
        (
            request.HospitalId,
            [phone],
            [email]
        );

        department.Name = request.Name;
        department.CreatedBy = "Admin-Test";

        await db.AddAsync(department);
        await db.SaveChangesAsync();

        return Results.Created($"/departments/{department.Id}", department.ToResponse());
    }

    private static async Task<IResult> Patch(int id, PatchDepartmentRequest request, HospitalTphDbContext db)
    {
        var department = await db.Departments
            .Include(e => e.PhoneNumbers)
            .Include(e => e.EmailAddresses)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (department is null)
            return Results.NotFound();

        if (request.Name is not null)
            department.Name = request.Name;

        department.UpdatedBy = "Admin-Test";

        if (request.PhoneNumber is not null)
        {
            foreach (var phone in department.PhoneNumbers.ToList()) department.RemovePhoneNumber(phone);
            var newPhone = new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label);
            department.AddPhoneNumber(newPhone);
        }

        if (request.Email is not null)
        {
            foreach (var email in department.EmailAddresses.ToList()) department.RemoveEmailAddress(email);
            var newEmail = new EmailAddress(request.Email.Value);
            department.AddEmailAddress(newEmail);
        }

        await db.SaveChangesAsync();
        return Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Update(int id, UpdateDepartmentRequest request, HospitalTphDbContext db)
    {
        var department = await db.Departments
            .Include(d => d.PhoneNumbers)
            .Include(d => d.EmailAddresses)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department is null)
            return Results.NotFound();

        department.Name = request.Name;
        department.UpdatedBy = "Admin-Test";

        var phone = new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label);
        var email = new EmailAddress(request.Email.Value);

        foreach (var p in department.PhoneNumbers.ToList()) department.RemovePhoneNumber(p);
        foreach (var e in department.EmailAddresses.ToList()) department.RemoveEmailAddress(e);

        department.AddPhoneNumber(phone);
        department.AddEmailAddress(email);

        await db.SaveChangesAsync();

        return Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Delete(int id, HospitalTphDbContext db)
    {
        var department = await db.Departments.FindAsync(id);
        if (department is null)
            return Results.NotFound();

        db.Departments.Remove(department);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
}