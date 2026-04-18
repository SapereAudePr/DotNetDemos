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

        builder.ToTable("Hospital");

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property<string>("_address")
            .HasMaxLength(256)
            .HasColumnName("Address")
            .IsRequired();
        
        builder.Property<DateTimeOffset>("_builtDate")
            .HasColumnName("BuiltDate")
            .IsRequired();
        
        
        builder.OwnsOne(x => x.MainPhoneNumber, pn =>
        {
            pn.Property(x => x.Number)
                .HasMaxLength(20)
                .HasColumnName("MainPhoneNumber")
                .IsRequired();
            
            pn.Property(x => x.Label)
                .HasMaxLength(120)
                .HasColumnName("Label")
                .IsRequired();
        });
        
        builder.Navigation(x => x.MainPhoneNumber)
            .HasField("_mainPhoneNumber")
            .UsePropertyAccessMode(PropertyAccessMode.Field);


        builder.OwnsOne(x => x.MainEmailAddress, ea =>
        {
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