using Domain.Entities.TPH;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.TPH;

public class HospitalConfiguration : AuditableEntityConfiguration<Hospital>
{
    public override void Configure(EntityTypeBuilder<Hospital> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Hospital", schema: "dbo");

        builder.HasIndex(x => x.Name)
            .IsUnique();
        

        builder.Property(x => x.Address)
            .HasField("_address")
            .HasColumnName("HospitalAddress")
            .HasMaxLength(256)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.Property(x => x.BuiltDate)
            .HasField("_builtDate")
            .HasPrecision(0)
            .HasColumnName("BuiltDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();

        builder.OwnsOne(x => x.MainPhoneNumber, pn =>
        {
            pn.Property(x => x.Number)
                .HasColumnName("MainPhoneNumber")
                .IsRequired()
                .HasMaxLength(20);

            pn.Property(x => x.Label)
                .HasColumnName("MainPhoneLabel")
                .HasMaxLength(120)
                .IsRequired();
        });

        builder.Navigation(x => x.MainPhoneNumber)
            .HasField("_mainPhoneNumber")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(x => x.MainEmailAddress, ea =>
        {
            ea.Property(x => x.Value)
                .HasColumnName("MainEmailAddress")
                .HasMaxLength(254)
                .IsRequired();
        });

        builder.Navigation(x => x.MainEmailAddress)
            .HasField("_mainEmailAddress")
            .UsePropertyAccessMode(PropertyAccessMode.Field);;
    }
}