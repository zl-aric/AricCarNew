using AricCar.Cars;
using AricCar.Regions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace AricCar.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ConnectionStringName("Default")]
public class AricCarDbContext :
    AbpDbContext<AricCarDbContext>,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Region> Regions { get; set; } = null!;

    public DbSet<Car> Cars { get; set; } = null!;

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext .
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    #endregion Entities from the modules

    public AricCarDbContext(DbContextOptions<AricCarDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(AricCarConsts.DbTablePrefix + "YourEntities", AricCarConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Region>(b =>
        {
            b.ToTable(AricCarConsts.DbTablePrefix + "Regions", AricCarConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.ProvincialCode).IsRequired().HasMaxLength(RegionConsts.RegionCodeMaxLength);
            b.Property(x => x.ProvincialName).IsRequired().HasMaxLength(RegionConsts.RegionNameMaxLength);
            b.Property(x => x.CityCode).IsRequired().HasMaxLength(RegionConsts.RegionCodeMaxLength);
            b.Property(x => x.CityName).IsRequired().HasMaxLength(RegionConsts.RegionNameMaxLength);
            b.Property(x => x.DistrictCode).IsRequired().HasMaxLength(RegionConsts.RegionCodeMaxLength);
            b.Property(x => x.DistrictName).IsRequired().HasMaxLength(RegionConsts.RegionNameMaxLength);
            b.HasAlternateKey(x => x.DistrictCode);

            //b.HasMany<Car>().WithOne().HasForeignKey(x => x.DistrctCode).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Car>(b =>
        {
            b.ToTable(AricCarConsts.DbTablePrefix + "Cars", AricCarConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.DistrctCode).IsRequired().HasMaxLength(RegionConsts.RegionCodeMaxLength);
            b.Property(x => x.Brand).IsRequired().HasMaxLength(CarConsts.MaxBranchLength);
            b.Property(x => x.Type).IsRequired().HasMaxLength(CarConsts.MaxTypeLength);
            b.Property(x => x.Description).IsRequired(false).HasMaxLength(CarConsts.MaxDescriptionLength);
            b.HasMany(x => x.Images).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.Region).WithMany().IsRequired().HasForeignKey(x => x.DistrctCode).HasPrincipalKey(x => x.DistrictCode).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CarImage>(b =>
        {
            b.ToTable(AricCarConsts.DbTablePrefix + "CarImages", AricCarConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Url).IsRequired().HasMaxLength(CarConsts.MaxImageUrlLength);
        });
    }
}