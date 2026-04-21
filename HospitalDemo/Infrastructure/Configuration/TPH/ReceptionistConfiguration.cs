using Domain.Entities.TPH;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
{
    public void Configure(EntityTypeBuilder<Receptionist> builder)
    {
        builder.OwnsMany(x => x.KnownLanguages, l =>
        {
            l.ToTable("ReceptionistLanguages");
            l.WithOwner().HasForeignKey("ReceptionistId");
            l.Property<int>("Id");
            l.HasKey("Id");
            
            l.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
            
            l.Property(x => x.Proficiency)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Property(x => x.DeskLocation)
            .HasField("_deskLocation")
            .HasMaxLength(30)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.HandlesInsuranceBilling)
            .HasDefaultValue(false)
            .IsRequired();
    }
}