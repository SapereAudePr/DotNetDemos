using Application.Common;
using Application.Queries.TPT;
using Application.Repositories.TPT;
using Application.Requests.TPT;
using Application.Validators.Validation;
using Domain.Entities.TPT;
using Domain.ValueObjects;

namespace Application.Services.TPT;

public class DepartmentTptService : IDepartmentTptService
{
    private readonly IDepartmentTptRepository _repo;

    public DepartmentTptService(IDepartmentTptRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<IEnumerable<Department>>> GetAllAsync(
        DepartmentQuery query, CancellationToken ct = default)
    {
        var departments = await _repo.GetAllAsync(query, ct);
        return Result<IEnumerable<Department>>.Success(departments);
    }

    public async Task<Result<Department>> GetByIdAsync(
        int id, CancellationToken ct = default)
    {
        var department = await _repo.GetByIdAsync(id, ct);

        if (department is null)
            return Result<Department>.NotFound($"Department with id {id} not found.");

        return Result<Department>.Success(department);
    }

    public async Task<Result<Department>> CreateAsync(
        CreateDepartmentRequest request, CancellationToken ct = default)
    {
        var validation = DepartmentTptValidation.ValidateCreate(request);
        if (!validation.IsValid)
            return Result<Department>.ValidationFailure(
                string.Join(", ", validation.Errors.Select(e => e.Message)));

        if (!await _repo.HospitalExistsAsync(request.HospitalId, ct))
            return Result<Department>.NotFound(
                $"Hospital with id {request.HospitalId} not found.");

        // Build entity
        var department = new Department(
            request.HospitalId,
            [new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label)],
            [new EmailAddress(request.Email.Value)])
        {
            Name = request.Name
        };

        var created = await _repo.CreateAsync(department, ct);
        return Result<Department>.Success(created);
    }

    public async Task<Result<Department>> UpdateAsync(
        int id, UpdateDepartmentRequest request, CancellationToken ct = default)
    {
        // Business rule 1: validate input
        var validation = DepartmentTptValidation.ValidateUpdate(request);
        if (!validation.IsValid)
            return Result<Department>.ValidationFailure(
                string.Join(", ", validation.Errors.Select(e => e.Message)));

        // Business rule 2: department must exist
        var department = await _repo.GetByIdAsync(id, ct);
        if (department is null)
            return Result<Department>.NotFound($"Department with id {id} not found.");

        // Mutate entity
        department.Name = request.Name;
        foreach (var p in department.PhoneNumbers.ToList()) department.RemovePhoneNumber(p);
        foreach (var e in department.EmailAddresses.ToList()) department.RemoveEmailAddress(e);
        department.AddPhoneNumber(new PhoneNumber(request.PhoneNumber.Number, request.PhoneNumber.Label));
        department.AddEmailAddress(new EmailAddress(request.EmailAddress.Value));

        var updated = await _repo.UpdateAsync(department, ct);
        return Result<Department>.Success(updated);
    }

    public async Task<Result<bool>> DeleteAsync(
        int id, CancellationToken ct = default)
    {
        var deleted = await _repo.DeleteAsync(id, ct);

        if (!deleted)
            return Result<bool>.NotFound($"Department with id {id} not found.");

        return Result<bool>.Success(true);
    }
}