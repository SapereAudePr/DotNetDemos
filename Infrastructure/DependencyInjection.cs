using Infrastructure.Interceptor;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditInterceptor>();

        services.AddDbContext<HospitalTphDbContext>((sp, options) =>
        {
            var auditInterceptor = sp.GetRequiredService<AuditInterceptor>();

            options.UseSqlServer
                    (configuration.GetConnectionString("TphDbConnection"))
                .AddInterceptors(auditInterceptor);
        });

        services.AddDbContext<HospitalTptDbContext>((sp, options) =>
        {
            var auditInterceptor = sp.GetRequiredService<AuditInterceptor>();

            options.UseSqlServer(
                    configuration.GetConnectionString("TptDbConnection"))
                .AddInterceptors(auditInterceptor);
        });

        return services;
    }
}