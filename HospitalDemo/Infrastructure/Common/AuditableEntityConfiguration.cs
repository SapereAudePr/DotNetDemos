using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common;

public abstract class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasPrecision(0)
            .ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior
                (Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

        builder.Property(x => x.UpdateDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasPrecision(0)
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(30)
            .IsRequired(false);
    }
}