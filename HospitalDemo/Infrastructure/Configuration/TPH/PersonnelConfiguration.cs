using Domain.Entities.TPH;
using Domain.Enums;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class PersonnelConfiguration : AuditableEntityConfiguration<Personnel>
{
    public override void Configure(EntityTypeBuilder<Personnel> builder)
    {
        base.Configure(builder);

        builder.ToTable("Personnel", schema: "Staff");

        builder.HasIndex(x => new { x.Name, x.DepartmentId });

        builder.HasDiscriminator<string>("PersonnelType")
            .HasValue<Doctor>("Doctor")
            .HasValue<Nurse>("Nurse")
            .HasValue<Technician>("Technician")
            .HasValue<Receptionist>("Receptionist")
            .HasValue<Janitor>("Janitor");

        builder.HasOne(x => x.Department)
            .WithMany(x => x.Personnel)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ShiftStart)
            .HasColumnName("ShiftStart")
            .HasField("_shiftStart")
            .HasPrecision(0)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.ShiftEnd)
            .HasColumnName("ShiftEnd")
            .HasField("_shiftEnd")
            .HasPrecision(0)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.Gender)
            .HasColumnName("Gender")
            .HasDefaultValue(Gender.Unknown)
            .IsRequired();

        builder.OwnsOne(x => x.PhoneNumber, pn =>
        {
            pn.ToTable("PersonnelPhone", schema: "Staff");
            pn.WithOwner().HasForeignKey("PersonnelId");
            pn.HasKey("PersonnelId");

            pn.Property(x => x.Number)
                .HasColumnName("PersonnelPhoneNumber")
                .HasMaxLength(20)
                .IsRequired();

            pn.Property(x => x.Label)
                .HasColumnName("PersonnelPhoneLabel")
                .HasMaxLength(120)
                .IsRequired();
        });

        builder.Navigation(x => x.PhoneNumber)
            .HasField("_phoneNumber")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(x => x.EmailAddress, ea =>
        {
            ea.Property("Value")
                .HasMaxLength(254)
                .IsRequired();
        });

        builder.Navigation(x => x.EmailAddress)
            .HasField("_emailAddress")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}