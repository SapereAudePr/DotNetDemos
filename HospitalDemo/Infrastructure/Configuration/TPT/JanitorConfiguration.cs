using Domain.Entities.TPT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class JanitorConfiguration : IEntityTypeConfiguration<Janitor>
{
    public void Configure(EntityTypeBuilder<Janitor> builder)
    {
        builder.ToTable("Janitors", schema: "Staff");


        builder.Property(x => x.AssignedZone)
            .HasColumnName("AssignedZone")
            .HasMaxLength(50)
            .HasField("_assignedZone")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.BiohazardCertified)
            .HasColumnName("BiohazardCertified");
        
        builder.Property(x => x.SecurityClearanceLevel)
            .HasColumnName("SecurityClearanceLevel")
            .HasMaxLength(50)
            .HasField("_securityClearanceLevel")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();
    }
}