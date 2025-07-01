using AricCar.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AricCar.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AricCarEntityFrameworkCoreModule),
    typeof(AricCarApplicationContractsModule)
)]
public class AricCarDbMigratorModule : AbpModule
{
}
