using Application.Queries.TPT;
using Application.Repositories.TPT;
using Domain.Entities.TPT;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.TPT;

public class DepartmentTptRepository : IDepartmentTptRepository
{
    private readonly HospitalTptDbContext _db;

    public DepartmentTptRepository(HospitalTptDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Department>> GetAllAsync(
        DepartmentQuery queryParams,
        CancellationToken ct = default)
    {
        var query = _db.Departments
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

        return await query
            .Skip(skip)
            .Take(queryParams.PageSize)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Department>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await _db.Departments
            .Include(d => d.PhoneNumbers)
            .Include(d => d.EmailAddresses)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Department?> GetByIdAsync(
        int id,
        CancellationToken ct = default)
    {
        return await _db.Departments
            .Include(d => d.PhoneNumbers)
            .Include(d => d.EmailAddresses)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<Department> CreateAsync(
        Department department,
        CancellationToken ct = default)
    {
        await _db.Departments.AddAsync(department, ct);
        await _db.SaveChangesAsync(ct);
        return department;
    }

    public async Task<Department> UpdateAsync(
        Department department,
        CancellationToken ct = default)
    {
        _db.Departments.Update(department);
        await _db.SaveChangesAsync(ct);
        return department;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken ct = default)
    {
        var entity = await _db.Departments.FindAsync([id], ct);
        if (entity is null) return false;
        _db.Departments.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> HospitalExistsAsync(
        int hospitalId,
        CancellationToken ct = default)
    {
        return await _db.Hospitals.AnyAsync(h => h.Id == hospitalId, ct);
    }
}