using Domain.Entities.TPT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
{
    public void Configure(EntityTypeBuilder<Technician> builder)
    {
        builder.ToTable("Technicians", schema: "Staff");
        
        builder.Property(x => x.TechnicalCategory)
            .HasColumnName("TechnicalCategory")
            .HasMaxLength(30)
            .HasField("_technicalCategory")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();
        
        builder.Property(x => x.EquipmentSpecialty)
            .HasColumnName("EquipmentSpecialty")
            .HasMaxLength(30)
            .HasField("_equipmentSpecialty")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();
        
        builder.Property(x => x.CertificationNumber)
            .HasColumnName("CertificationNumber")
            .HasMaxLength(80)
            .HasField("_certificationNumber")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();
    }
}