using Domain.Entities.TPH;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class PersonnelConfiguration : AuditableEntityConfiguration<Personnel>
{
    public override void Configure(EntityTypeBuilder<Personnel> builder)
    {
        base.Configure(builder);

        builder.ToTable("Personnel");

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

        builder.Property<DateTime>("_shiftStart")
            .HasColumnName("ShiftStart")
            .IsRequired();

        builder.Property<DateTime>("_shiftEnd")
            .HasColumnName("ShiftEnd")
            .IsRequired();

        builder.Property(x => x.Gender)
            .IsRequired();

        builder.OwnsOne(x => x.PhoneNumber, pn =>
        {
            pn.Property<string>("Number")
                .HasMaxLength(20)
                .IsRequired();

            pn.Property<string>("Label")
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