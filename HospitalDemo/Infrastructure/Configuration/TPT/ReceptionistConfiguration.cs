using Domain.Entities.TPT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
{
    public void Configure(EntityTypeBuilder<Receptionist> builder)
    {
        builder.ToTable("Receptionists", schema: "Staff");

        builder.Property(x => x.DeskLocation)
            .HasColumnName("DeskLocation")
            .HasMaxLength(30)
            .HasField("_deskLocation")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.HandlesInsuranceBilling)
            .HasColumnName("HandlesInsuranceBilling");


        builder.OwnsMany(x => x.KnownLanguages, l =>
        {
            l.ToTable("ReceptionistLanguages", schema: "Staff");
            l.WithOwner().HasForeignKey("ReceptionistId");
            l.Property<int>("Id");
            l.HasKey("Id", "ReceptionistId");

            l.Property(x => x.Name)
                .HasColumnName("LanguageName")
                .HasMaxLength(50)
                .IsRequired();

            l.Property(x => x.Proficiency)
                .HasColumnName("LanguageProficiency")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Navigation(x => x.KnownLanguages)
            .HasField("_knownLanguages")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}