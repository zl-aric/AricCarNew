using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AricCar.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class AricCarDbContextFactory : IDesignTimeDbContextFactory<AricCarDbContext>
{
    public AricCarDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        AricCarEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<AricCarDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));
        
        return new AricCarDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AricCar.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
