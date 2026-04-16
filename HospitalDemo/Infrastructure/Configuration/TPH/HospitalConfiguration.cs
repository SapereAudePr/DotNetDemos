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
        
        builder.ToTable("Hospital");

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany(x => x.Departments)
            .WithOne(x => x.Hospital)
            .HasForeignKey(x => x.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property<string>("_address")
            .HasColumnName("Address")
            .IsRequired()
            .HasMaxLength(256);

        builder.Property<DateTimeOffset>("_builtDate")
            .HasColumnName("BuiltDate")
            .IsRequired();

        builder.OwnsOne(x => x.MainPhoneNumber, pn =>
        {
            pn.Property(x => x.Number)
                .HasColumnName("MainPhoneNumber")
                .IsRequired()
                .HasMaxLength(20);

            pn.Property(x => x.Label)
                .HasColumnName("MainPhoneLabel")
                .IsRequired()
                .HasMaxLength(120);
        });

        builder.Navigation(x => x.MainPhoneNumber)
            .HasField("_mainPhoneNumber")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(x => x.MainEmailAddress, ea =>
        {
            ea.Property(x => x.Value)
                .HasColumnName("MainEmailAddress")
                .IsRequired()
                .HasMaxLength(254);
        });

        builder.Navigation(x => x.MainEmailAddress)
            .HasField("_mainEmailAddress")
            .UsePropertyAccessMode(PropertyAccessMode.Field);;
    }
}