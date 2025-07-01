using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AricCar.Data;

/* This is used if database provider does't define
 * IAricCarDbSchemaMigrator implementation.
 */
public class NullAricCarDbSchemaMigrator : IAricCarDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
