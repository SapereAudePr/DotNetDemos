using Application.Common;
using Application.Queries.TPT;
using Application.Requests.TPT;
using Domain.Entities.TPT;

namespace Application.Services.TPT;

public interface IDepartmentTptService
{
    Task<Result<IEnumerable<Department>>> GetAllAsync(DepartmentQuery query, CancellationToken ct = default);
    Task<Result<Department>> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Result<Department>> CreateAsync(CreateDepartmentRequest request, CancellationToken ct = default);
    Task<Result<Department>> UpdateAsync(int id, UpdateDepartmentRequest request, CancellationToken ct = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken ct = default);
}