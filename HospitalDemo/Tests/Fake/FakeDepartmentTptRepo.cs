using Application.Queries.TPT;
using Application.Repositories.TPT;
using Domain.Entities.TPT;

namespace Tests.Fake;

public class FakeDepartmentTptRepo : IDepartmentTptRepository
{
    private readonly List<Department> _departments;
    private readonly List<int> _hospitalIds;

    public FakeDepartmentTptRepo(
        List<Department>? departments = null,
        List<int>? hospitalIds = null)
    {
        _departments = departments ?? new List<Department>();
        _hospitalIds = hospitalIds ?? new List<int>();
    }

    public Task<bool> HospitalExistsAsync(int departmentId, CancellationToken ct = default)
    {
        return Task.FromResult(_hospitalIds.Contains(departmentId));
    }


    public Task<IEnumerable<Department>> GetAllAsync(DepartmentQuery q, CancellationToken ct = default)
    {
        var result = _departments.AsEnumerable();

        if (!string.IsNullOrEmpty(q.FilterOn)
            && q.FilterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
            result = result.Where(x => x.Name.Contains(q.FilterQuery!));

        if (!string.IsNullOrEmpty(q.SortBy) &&
            q.SortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
            result =
                q.SortAscending
                    ? result.OrderBy(x => x.Name)
                    : result.OrderByDescending(x => x.Name);


        if (!string.IsNullOrEmpty(q.SortBy) &&
            q.SortBy.Equals("id", StringComparison.OrdinalIgnoreCase))
            result =
                q.SortAscending
                    ? result.OrderBy(x => x.Id)
                    : result.OrderByDescending(x => x.Id);

        result = result
            .Skip((q.PageNumber - 1) * q.PageSize)
            .Take(q.PageSize);

        return Task.FromResult<IEnumerable<Department>>(result);
    }

    public Task<IEnumerable<Department>> GetAllAsync(CancellationToken ct = default)
    {
        return Task.FromResult(_departments.AsEnumerable());
    }

    public Task<Department?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return Task.FromResult(_departments.FirstOrDefault(x => x.Id == id));
    }

    public Task<Department> CreateAsync(Department entity, CancellationToken ct = default)
    {
        var nextId = _departments.Count > 0 ? _departments.Max(x => x.Id) + 1 : 1;

        _departments.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Department> UpdateAsync(Department entity, CancellationToken ct = default)
    {
        var index = _departments.FindIndex(x => x.Id == entity.Id);

        if (index >= 0)
            _departments[index] = entity;

        return Task.FromResult(entity);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var find = _departments.FirstOrDefault(x => x.Id == id);

        if (find is null)
            return Task.FromResult(false);

        _departments.Remove(find);

        return Task.FromResult(true);
    }
}