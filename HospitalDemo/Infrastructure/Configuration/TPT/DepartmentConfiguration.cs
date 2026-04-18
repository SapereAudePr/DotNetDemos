using Domain.Entities.TPT;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class DepartmentConfiguration : AuditableEntityConfiguration<Department>
{
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        base.Configure(builder);

        builder.ToTable("Departments");

        builder.HasIndex(d => new { d.Name, d.HospitalId })
            .IsUnique();

        builder.HasOne(x => x.Hospital)
            .WithMany(x => x.Departments)
            .HasForeignKey(x => x.HospitalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);


        builder.OwnsMany(x => x.PhoneNumbers, pn =>
        {
            pn.ToTable("DepartmentPhoneNumbers");
            pn.WithOwner().HasForeignKey("DepartmentId");
            pn.Property<int>("Id");
            pn.HasKey("Id");

            pn.Property(x => x.Number)
                .HasMaxLength(20)
                .IsRequired();

            pn.Property(x => x.Label)
                .HasMaxLength(120)
                .IsRequired();
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
            
            
            ea.Property(x => x.Value)
                .HasMaxLength(254)
                .IsRequired();
        });
        
        builder.Navigation(x => x.EmailAddresses)
            .HasField("_emailAddresses")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}