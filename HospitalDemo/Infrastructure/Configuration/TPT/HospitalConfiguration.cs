using Domain.Entities.TPT;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPT;

public class HospitalConfiguration : AuditableEntityConfiguration<Hospital>
{
    public override void Configure(EntityTypeBuilder<Hospital> builder)
    {
        base.Configure(builder);

        builder.ToTable("Hospital", schema: "dbo");

        builder.HasIndex(x => x.Name)
            .IsUnique();
        

        builder.Property(x => x.Address)
            .HasColumnName("HospitalAddress")
            .HasMaxLength(256)
            .HasField("_address")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.BuiltDate)
            .HasColumnName("HospitalBuiltDate")
            .HasField("_builtDate")
            .HasPrecision(0)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();


        builder.OwnsOne(x => x.MainPhoneNumber, pn =>
        {
            pn.ToTable("HospitalPhoneNumber", schema: "dbo");

            pn.WithOwner().HasForeignKey("HospitalId");
            pn.HasKey("HospitalId");

            pn.Property(x => x.Number)
                .HasMaxLength(20)
                .HasColumnName("MainPhoneNumber")
                .IsRequired();

            pn.Property(x => x.Label)
                .HasMaxLength(120)
                .HasColumnName("MainPhoneLabel")
                .IsRequired();
        });

        builder.Navigation(x => x.MainPhoneNumber)
            .HasField("_mainPhoneNumber")
            .UsePropertyAccessMode(PropertyAccessMode.Field);


        builder.OwnsOne(x => x.MainEmailAddress, ea =>
        {
            ea.ToTable("HospitalEmailAddress", schema: "dbo");

            ea.WithOwner().HasForeignKey("HospitalId");
            ea.HasKey("HospitalId");

            ea.Property(x => x.Value)
                .HasMaxLength(254)
                .HasColumnName("MainEmailAddress")
                .IsRequired();
        });

        builder.Navigation(x => x.MainEmailAddress)
            .HasField("_mainEmailAddress")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}