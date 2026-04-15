using Domain.Entities.TPH;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class JanitorConfiguration : IEntityTypeConfiguration<Janitor>
{
    public void Configure(EntityTypeBuilder<Janitor> builder)
    {
        builder.Property(x => x.AssignedZone)
            .HasField("_assignedZone")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.BiohazardCertified)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.SecurityClearanceLevel)
            .HasField("_securityClearanceLevel")
            .HasMaxLength(50)
            .IsRequired();
    }
}