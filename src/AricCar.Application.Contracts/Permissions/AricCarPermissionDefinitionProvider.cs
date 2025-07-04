using AricCar.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AricCar.Permissions;

public class AricCarPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AricCarPermissions.GroupName);

        var regionPermission = myGroup.AddPermission(AricCarPermissions.Regions.Default, L("区域管理"));
        regionPermission.AddChild(AricCarPermissions.Regions.Create, L("Permission:Create"));
        regionPermission.AddChild(AricCarPermissions.Regions.Edit, L("Permission:Edit"));
        regionPermission.AddChild(AricCarPermissions.Regions.Delete, L("Permission:Delete"));


        var carPermission = myGroup.AddPermission(AricCarPermissions.Cars.Default, L("车辆管理"));
        carPermission.AddChild(AricCarPermissions.Cars.Create, L("Permission:Create"));
        carPermission.AddChild(AricCarPermissions.Cars.Edit, L("Permission:Edit"));
        carPermission.AddChild(AricCarPermissions.Cars.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AricCarResource>(name);
    }
}