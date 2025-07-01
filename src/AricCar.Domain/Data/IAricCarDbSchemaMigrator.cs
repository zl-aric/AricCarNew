using System.Threading.Tasks;

namespace AricCar.Data;

public interface IAricCarDbSchemaMigrator
{
    Task MigrateAsync();
}
