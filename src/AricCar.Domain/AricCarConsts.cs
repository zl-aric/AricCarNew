using Volo.Abp.Identity;

namespace AricCar;

public static class AricCarConsts
{
    public const string DbTablePrefix = "App";
    public const string? DbSchema = null;


    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;
}
