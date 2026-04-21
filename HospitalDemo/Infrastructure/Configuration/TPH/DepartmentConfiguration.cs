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

        builder.ToTable("Department", schema: "Staff");

        builder.HasIndex(d => new { d.Name, d.HospitalId })
            .IsUnique();

        builder.HasOne(x => x.Hospital)
            .WithMany(x => x.Departments)
            .HasForeignKey(x => x.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);
        

        builder.OwnsMany(x => x.PhoneNumbers, pn =>
        {
            pn.ToTable("DepartmentPhoneNumbers", schema: "Staff");
            pn.WithOwner().HasForeignKey("DepartmentId");
            pn.Property<int>("Id");
            pn.HasKey("Id", "DepartmentId");
            
            pn.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(20);

            pn.Property(x => x.Label)
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
            ea.ToTable("DepartmentEmailAddresses", schema: "Staff");
            
            ea.WithOwner().HasForeignKey("DepartmentId");
            ea.Property<int>("Id");
            ea.HasKey("Id", "DepartmentId");
            
            
            ea.Property(x => x.Value)
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