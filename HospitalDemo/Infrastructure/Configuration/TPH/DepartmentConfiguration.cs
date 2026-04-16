using Domain.Entities.TPH;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class DepartmentConfiguration : AuditableEntityConfiguration<Department>
{
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        base.Configure(builder);

        builder.ToTable("Department");

        builder.HasIndex(d => new { d.Name, d.HospitalId })
            .IsUnique();

        builder.HasOne(x => x.Hospital)
            .WithMany(x => x.Departments)
            .HasForeignKey(x => x.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Personnel)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(x => x.PhoneNumbers , pn =>
        {
            pn.ToTable("DepartmentPhoneNumbers");
            pn.WithOwner().HasForeignKey("DepartmentId");
            pn.Property<int>("Id");
            pn.HasKey("Id");
            pn.Property("Number")
            .IsRequired()
            .HasMaxLength(20);

            pn.Property("Label")
            .HasMaxLength(120)
            .IsRequired();

            pn.HasIndex("DepartmentId");
            pn.HasIndex("DepartmentId", "Number", "Label").IsUnique();
        });

        builder.Navigation(x => x.PhoneNumbers) 
            .HasField("_phoneNumbers")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(x => x.EmailAddresses, ea =>
        {
            ea.ToTable("DepartmentEmailAddresses");
            ea.WithOwner().HasForeignKey("DepartmentId");
            ea.Property<int>("Id");
            ea.HasKey("Id");
            ea.Property("Value")
            .IsRequired()
            .HasMaxLength(254);

            ea.HasIndex("DepartmentId");
            ea.HasIndex("DepartmentId", "Value").IsUnique();
        });

        builder.Navigation(x => x.EmailAddresses)
            .HasField("_emailAddresses")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
