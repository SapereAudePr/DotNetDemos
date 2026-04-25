using Domain.Entities.TPT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
{
    public void Configure(EntityTypeBuilder<Nurse> builder)
    {
        builder.ToTable("Nurses", schema: "Staff");
        
        builder.Property(x => x.IsHeadNurse)
            .HasColumnName("IsHeadNurse")
            .HasDefaultValue(false)
            .HasField("_isHeadNurse")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();
        
        builder.Property(x => x.CertificationLevel)
            .HasColumnName("CertificationLevel")
            .HasMaxLength(30)
            .HasField("_certificationLevel")
            .IsRequired();
        
        builder.Property(x => x.AssignedWard)
            .HasColumnName("AssignedWard")
            .HasMaxLength(30)
            .HasField("_assignedWard")
            .IsRequired();
        
        builder.Property(x => x.ShiftType)
            .HasColumnName("ShiftType")
            .HasMaxLength(30)
            .HasField("_shiftType")
            .IsRequired();
    }
}