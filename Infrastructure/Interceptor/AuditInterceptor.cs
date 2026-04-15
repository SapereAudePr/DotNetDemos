using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptor;

public class AuditInterceptor : SaveChangesInterceptor
{
    private void ApplyAuditRules(DbContext? dbContext)
    {
        if (dbContext is null) return;

        var entries = dbContext.ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            var now = DateTimeOffset.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreationDate).CurrentValue = now;
                entry.Property(x => x.UpdateDate).CurrentValue = now;

                entry.Property(x => x.CreationDate).IsModified = false;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.CreationDate).IsModified = false;
                entry.Property(x => x.UpdateDate).CurrentValue = now;
            }
        }
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        ApplyAuditRules(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditRules(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}