using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AricCar.Data;
using Volo.Abp.DependencyInjection;

namespace AricCar.EntityFrameworkCore;

public class EntityFrameworkCoreAricCarDbSchemaMigrator
    : IAricCarDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAricCarDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AricCarDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AricCarDbContext>()
            .Database
            .MigrateAsync();
    }
}
