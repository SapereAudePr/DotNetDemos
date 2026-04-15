using Domain.Entities.TPH;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
{
    public void Configure(EntityTypeBuilder<Technician> builder)
    {
        builder.Property(x => x.TechnicalCategory)
            .HasField("_technicalCategory")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.EquipmentSpecialty)
            .HasField("_equipmentSpecialty")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.CertificationNumber)
            .HasField("_certificationNumber")
            .HasMaxLength(80)
            .IsRequired();
    }
}