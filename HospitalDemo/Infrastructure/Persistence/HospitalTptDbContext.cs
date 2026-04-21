using Domain.Entities.TPT;
using Infrastructure.Configuration.TPT;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class HospitalTptDbContext(DbContextOptions<HospitalTptDbContext> options) : DbContext(options)
{
    public DbSet<Hospital> Hospitals => Set<Hospital>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Personnel> Personnel => Set<Personnel>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Personnel>().UseTptMappingStrategy();
        
        // scans the entire project for any class that implements IEntityTypeConfiguration<T>
        
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalTptDbContext).Assembly);
     
        // Only classes that implement custom 'ITptConfiguration' interface will be registered.
        
        // modelBuilder.ApplyConfigurationsFromAssembly( 
        //     typeof(HospitalTptDbContext).Assembly,
        //     t => t.GetInterfaces().Contains(typeof(ITptConfiguration)));

        
        // Manual
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