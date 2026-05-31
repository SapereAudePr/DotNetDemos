using Domain.Entities.TPT;
using Api.Mappings.TPT;
using Api.Requests.TPT;
using Api.Validators.Validation;
using Application.Queries.TPT;
using Application.Repositories.TPT;
using Domain.ValueObjects;

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

    private static async Task<IResult> GetAll(
        [AsParameters] DepartmentQuery queryParams,
        IDepartmentTptRepository repo)
    {
        var departments = await repo.GetAllAsync(queryParams);
        return Results.Ok(departments.ToResponse());
    }

    private static async Task<IResult> GetById(
        int id,
        IDepartmentTptRepository repo)
    {
        var department = await repo.GetByIdAsync(id);
        return department is null ? Results.NotFound() : Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Create(
        CreateDepartmentRequest request,
        IDepartmentTptRepository repo)
    {
        var validation = DepartmentTptValidation.ValidateCreate(request);
        if (!validation.IsValid)
            return Results.ValidationProblem(
                validation.Errors.ToDictionary(e => e.Field,
                    e => new[] { e.Message }));

        if (!await repo.HospitalExistsAsync(request.HospitalId))
            return Results.BadRequest($"Hospital with id {request.HospitalId} not found.");


        var department = new Department(
            request.HospitalId,
            [new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label)],
            [new EmailAddress(request.Email.Value)])
        {
            Name = request.Name
        };

        var created = await repo.CreateAsync(department);
        return Results.Created($"/tpt/departments/{created.Id}", created.ToResponse());
    }

    private static async Task<IResult> Update(
        int id,
        UpdateDepartmentRequest request,
        IDepartmentTptRepository repo)
    {
        var validation = DepartmentTptValidation.ValidateUpdate(request);
        if (!validation.IsValid)
            return Results.ValidationProblem(
                validation.Errors.ToDictionary(e => e.Field,
                    e => new[] { e.Message }));

        var department = await repo.GetByIdAsync(id);
        if (department is null)
            return Results.NotFound();

        department.Name = request.Name;
        foreach (var p in department.PhoneNumbers.ToList()) department.RemovePhoneNumber(p);
        foreach (var e in department.EmailAddresses.ToList()) department.RemoveEmailAddress(e);
        department.AddPhoneNumber(new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label));
        department.AddEmailAddress(new EmailAddress(request.EmailAddress.Value));

        await repo.UpdateAsync(department);
        return Results.Ok(department.ToResponse());
    }

    private static async Task<IResult> Delete(int id, IDepartmentTptRepository repo)
    {
        var department = await repo.DeleteAsync(id);
        return department ? Results.NoContent() : Results.NotFound();
    }
}