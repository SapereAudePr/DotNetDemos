using Application.Queries.TPT;
using Domain.Entities.TPT;

namespace Application.Repositories.TPT;

public interface IDepartmentTptRepository : IRepository<Department>
{
    Task<IEnumerable<Department>> GetAllAsync(DepartmentQuery queryParams, CancellationToken ct = default);
    Task<bool> HospitalExistsAsync(int departmentId, CancellationToken ct = default);
}
