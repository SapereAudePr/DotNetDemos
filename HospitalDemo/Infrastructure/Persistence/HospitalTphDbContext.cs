using System.Reflection;
using Domain.Entities.TPH;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class HospitalTphDbContext(DbContextOptions<HospitalTphDbContext> options) : DbContext(options)
{
    public DbSet<Hospital> Hospitals => Set<Hospital>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Personnel> Personnel => Set<Personnel>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalTphDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}
