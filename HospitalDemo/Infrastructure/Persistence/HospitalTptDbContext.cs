using Domain.Entities.TPT;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class HospitalTptDbContext(DbContextOptions<HospitalTptDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Personnel>().UseTptMappingStrategy();
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalTptDbContext).Assembly);
    }
}