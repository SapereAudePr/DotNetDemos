using Domain.Entities.TPH;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
{
    public void Configure(EntityTypeBuilder<Receptionist> builder)
    {
        builder.ToTable("Receptionists", schema: "Staff");

        builder.OwnsMany(x => x.KnownLanguages, l =>
        {
            l.ToTable("ReceptionistLanguages");
            l.WithOwner().HasForeignKey("ReceptionistId");
            l.Property<int>("Id");
            l.HasKey("Id");

            l.Property(x => x.Name)
                .HasColumnName("LanguageName")
                .HasMaxLength(50)
                .IsRequired();

            l.Property(x => x.Proficiency)
                .HasColumnName("ProficiencyLevel")
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