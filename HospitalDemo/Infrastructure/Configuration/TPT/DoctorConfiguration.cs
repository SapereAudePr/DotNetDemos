using Domain.Entities.TPT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors", schema: "Staff");

        builder.Property(d => d.Specialization)
            .HasColumnName("Specialization")
            .HasMaxLength(50)
            .HasField("_specialization")
            .IsRequired();

        builder.Property(d => d.LicenseNumber)
            .HasColumnName("LicenseNumber")
            .HasMaxLength(50)
            .HasField("_licenseNumber")
            .IsRequired();
    }
}