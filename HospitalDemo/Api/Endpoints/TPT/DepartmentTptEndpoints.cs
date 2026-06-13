using Api.Extensions;
using Domain.Entities.TPT;
using Api.Mappings.TPT;
using Application.Queries.TPT;
using Application.Requests.TPT;
using Application.Services.TPT;

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
        IDepartmentTptService service)
    {
        var result = await service.GetAllAsync(queryParams);
        return result.ToHttpResult(departments => Results.Ok(departments.ToResponse()));
    }

    private static async Task<IResult> GetById(
        int id,
        IDepartmentTptService service)
    {
        var result = await service.GetByIdAsync(id);
        return result.ToHttpResult(department => Results.Ok(department.ToResponse()));
    }

    private static async Task<IResult> Create(
        CreateDepartmentRequest request,
        IDepartmentTptService service)
    {
        var result = await service.CreateAsync(request);
        return result.ToHttpResult(department =>
            Results.Created($"/tpt/departments/{department.Id}", department.ToResponse()));
    }

    private static async Task<IResult> Update(
        int id,
        UpdateDepartmentRequest request,
        IDepartmentTptService service)
    {
        var result = await service.UpdateAsync(id, request);
        return result.ToHttpResult(department => Results.Ok(department.ToResponse()));
    }

    private static async Task<IResult> Delete(int id, IDepartmentTptService service)
    {
        var result = await service.DeleteAsync(id);
        return result.ToHttpResult(_ => Results.NoContent());
    }
}