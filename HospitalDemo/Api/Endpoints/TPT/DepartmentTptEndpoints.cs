using Api.Queries.TPT;
using Domain.Entities.TPT;
using Api.Mappings.TPT;
using Api.Requests.TPH;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Endpoints.TPT;

public static class DepartmentTptEndpoints
{
    public static IEndpointRouteBuilder MapDepartmentTptRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tpt/departments")
            .WithTags("TPT - Departments");

        group.MapGet("/", GetAll)
            .WithName("TPT.Departments.GetAll")
            .WithSummary("Returns all departments")
            .WithDescription("")
            .Produces<List<Department>>(200);

        group.MapGet("/{id:int}", GetById)
            .WithName("TPT.Departments.GetById")
            .WithSummary("Gets a department by Id")
            .WithDescription("Returns 404 if not found")
            .Produces<Department>(200)
            .Produces(404);

        group.MapPost("/", Create)
            .WithName("TPT.Departments.Create")
            .WithSummary("Creates a new department")
            .WithDescription("")
            .Produces<Department>(201);

        group.MapPatch("/{id:int}", Patch)
            .WithName("TPT.Departments.Patch")
            .WithSummary("Patches a department")
            .WithDescription(
                "If PhoneNumber or Email is included in the request, existing values are replaced with the new value.")
            .Produces<Department>(200)
            .Produces(404);

        group.MapPut("/{id:int}", Update)
            .WithName("TPT.Departments.Update")
            .WithSummary("Updates a department")
            .WithDescription(
                "PhoneNumber and Email's existing values are replaced with the new values.")
            .Produces<Department>(200)
            .Produces(404);

        group.MapDelete("/{id:int}", Delete)
            .WithName("TPT.Departments.Delete")
            .WithSummary("Deletes a department")
            .WithDescription("Returns 404 if not found and 204 if found and deleted")
            .Produces(204)
            .Produces(404);

        return app;
    }

    private static async Task<IResult> GetAll([AsParameters] DepartmentQuery queryParams, HospitalTptDbContext db)
    {
        var query = db.Departments
            .Include(d => d.PhoneNumbers)
            .Include(d => d.EmailAddresses)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.FilterOn) &&
            queryParams.FilterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.Name.Contains(queryParams.FilterQuery!));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.SortBy) &&
            queryParams.SortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
        {
            query = queryParams.SortAscending
                ? query.OrderBy(x => x.Name)
                : query.OrderByDescending(x => x.Name);
        }

        var skip = (queryParams.PageNumber - 1) * queryParams.PageSize;

        var departments = await query
            .Skip(skip)
            .Take(queryParams.PageSize)
            .AsNoTracking()
            .ToListAsync();

        return Results.Ok(departments.ToResponse());
    }

    private static async Task<IResult> GetById(int id, HospitalTptDbContext db)
    {
        var department = await db.Departments
            .Include(d => d.PhoneNumbers)
            .Include(d => d.EmailAddresses)
            .FirstOrDefaultAsync(d => d.Id == id);

        return department is null
            ? Results.NotFound()
            : Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Create(CreateDepartmentRequest request, HospitalTptDbContext db)
    {
        var department = new Department(
            request.HospitalId,
            [new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label)],
            [new EmailAddress(request.Email.Value)]
        )
        {
            Name = request.Name,
            CreatedBy = "Admin-Test"
        };

        await db.AddAsync(department);
        await db.SaveChangesAsync();

        return Results.Created($"/tpt/departments/{department.Id}", department.ToResponse());
    }

    private static async Task<IResult> Patch(int id, PatchDepartmentRequest request, HospitalTptDbContext db)
    {
        var department = await db.Departments
            .Include(d => d.PhoneNumbers)
            .Include(d => d.EmailAddresses)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department is null)
            return Results.NotFound();

        if (request.Name is not null)
            department.Name = request.Name;

        if (request.PhoneNumber is not null)
        {
            foreach (var p in department.PhoneNumbers.ToList())
            {
                department.RemovePhoneNumber(p);
            }

            department.AddPhoneNumber(new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label));
        }

        if (request.Email is not null)
        {
            foreach (var e in department.EmailAddresses.ToList())
            {
                department.RemoveEmailAddress(e);
            }

            department.AddEmailAddress(new EmailAddress(request.Email.Value));
        }

        department.UpdatedBy = "Admin-Test";

        await db.SaveChangesAsync();

        return Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Update(int id, UpdateDepartmentRequest request, HospitalTptDbContext db)
    {
        var department = await db.Departments
            .Include(d => d.PhoneNumbers)
            .Include(d => d.EmailAddresses)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department is null)
            return Results.NotFound();

        department.Name = request.Name;


        foreach (var p in department.PhoneNumbers.ToList())
        {
            department.RemovePhoneNumber(p);
        }

        foreach (var e in department.EmailAddresses.ToList())
        {
            department.RemoveEmailAddress(e);
        }

        department.AddPhoneNumber(new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label));
        department.AddEmailAddress(new EmailAddress(request.Email.Value));

        department.UpdatedBy = "Admin-Test";

        await db.SaveChangesAsync();

        return Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Delete(int id, HospitalTptDbContext db)
    {
        var department = await db.Departments.FindAsync(id);

        if (department is null)
            return Results.NotFound();

        db.Departments.Remove(department);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
}