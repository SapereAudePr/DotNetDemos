using Domain.Entities.TPH;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
{
    public void Configure(EntityTypeBuilder<Nurse> builder)
    {
        builder.Property(x => x.IsHeadNurse)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.CertificationLevel)
            .HasField("_certificationLevel")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.AssignedWard)
            .HasField("_assignedWard")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.ShiftType)
            .HasField("_shiftType")
            .HasMaxLength(30)
            .IsRequired();
    }
}
