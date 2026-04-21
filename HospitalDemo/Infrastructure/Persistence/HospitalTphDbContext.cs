using Domain.Entities.TPH;
using Infrastructure.Configuration.TPH;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class HospitalTphDbContext(DbContextOptions<HospitalTphDbContext> options) : DbContext(options)
{
    public DbSet<Hospital> Hospitals => Set<Hospital>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Personnel> Personnel => Set<Personnel>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalTphDbContext).Assembly);

        modelBuilder.ApplyConfiguration(new HospitalConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new PersonnelConfiguration());
        modelBuilder.ApplyConfiguration(new JanitorConfiguration());
        modelBuilder.ApplyConfiguration(new ReceptionistConfiguration());
        modelBuilder.ApplyConfiguration(new TechnicianConfiguration());
        modelBuilder.ApplyConfiguration(new NurseConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
    }
}
