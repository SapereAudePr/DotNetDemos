using Domain.Entities.TPT;
using Domain.Enums;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class PersonnelConfiguration : AuditableEntityConfiguration<Personnel>
{
    public override void Configure(EntityTypeBuilder<Personnel> builder)
    {
        base.Configure(builder);

        builder.ToTable("Personnel", schema: "Staff",
            t => t.HasComment
                ("Primary table for TPT hierarchy of all staff members"));

        builder.HasIndex(x => new { x.Name, x.DepartmentId });

        builder.HasOne(x => x.Department)
            .WithMany(x => x.Personnel)
            .HasForeignKey(x => x.DepartmentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ShiftStart)
            .HasField("_shiftStart")
            .HasPrecision(0)
            .HasColumnName("ShiftStart")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.ShiftEnd)
            .HasField("_shiftEnd")
            .HasPrecision(0)
            .HasColumnName("ShiftEnd")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.Gender)
            .HasColumnName("Gender")
            .HasColumnType("tinyint")
            .HasDefaultValue(0)
            .IsRequired();

        builder.OwnsOne(x => x.PhoneNumber, pn =>
        {
            pn.ToTable("PersonnelPhone", schema: "Staff");
            pn.WithOwner().HasForeignKey("PersonnelId");
            pn.HasKey("PersonnelId");
            
            pn.Property(x => x.Number)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(20)
                .IsRequired();

            pn.Property(x => x.Label)
                .HasColumnName("PhoneLabel")
                .HasMaxLength(120)
                .IsRequired();
        });

        builder.Navigation(x => x.PhoneNumber)
            .HasField("_phoneNumber")
            .UsePropertyAccessMode(PropertyAccessMode.Field);


        builder.OwnsOne(x => x.EmailAddress, ea =>
        {
            ea.ToTable("PersonnelEmailAddress", schema: "Staff");
            ea.WithOwner().HasForeignKey("PersonnelId");
            ea.HasKey("PersonnelId");
            
            ea.Property(x => x.Value)
                .HasColumnName("EmailAddress")
                .HasMaxLength(254)
                .IsRequired();
        });

        builder.Navigation(x => x.EmailAddress)
            .HasField("_emailAddress")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}